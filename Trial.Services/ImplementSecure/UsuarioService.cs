using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Trial.AppInfra;
using Trial.AppInfra.EmailHelper;
using Trial.AppInfra.ErrorHandling;
using Trial.AppInfra.Extensions;
using Trial.AppInfra.FileHelper;
using Trial.AppInfra.Transactions;
using Trial.AppInfra.UserHelper;
using Trial.Domain.EntitesSoftSec;
using Trial.Domain.Entities;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesSecure;

namespace Trial.Services.ImplementSecure;

public class UsuarioService : IUsuarioService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IFileStorage _fileStorage;
    private readonly IUserHelper _userHelper;
    private readonly IEmailHelper _emailHelper;
    private readonly ImgSetting _imgOption;
    private readonly IStringLocalizer _localizer;

    public UsuarioService(DataContext context, IHttpContextAccessor httpContextAccessor, HttpErrorHandler httpErrorHandle,
        ITransactionManager transactionManager, IFileStorage fileStorage, IStringLocalizer localizer,
        IUserHelper userHelper, IEmailHelper emailHelper, IOptions<ImgSetting> ImgOption)
    {
        _context = context;
        _httpErrorHandler = httpErrorHandle;
        _httpContextAccessor = httpContextAccessor;
        _transactionManager = transactionManager;
        _fileStorage = fileStorage;
        _userHelper = userHelper;
        _emailHelper = emailHelper;
        _imgOption = ImgOption.Value;
        _localizer = localizer;
    }

    public async Task<ActionResponse<IEnumerable<Usuario>>> GetAsync(PaginationDTO pagination, string Email)
    {
        try
        {
            User user = await _userHelper.GetUserAsync(Email);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<Usuario>>
                {
                    WasSuccess = false,
                    Message = "Problemas para Conseguir el Usuario"
                };
            }
            var queryable = _context.Usuarios.Include(x => x.UsuarioRoles).Where(x => x.CorporationId == user.CorporationId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                //Busqueda grandes mateniendo los indices de los campos, campo Esta Collation CI para Case Insensitive
                queryable = queryable.Where(u => EF.Functions.Like(u.FullName, $"%{pagination.Filter}%"));
            }
            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.FullName).Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<Usuario>>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Usuario>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Usuario>> GetAsync(int id)
    {
        try
        {
            var modelo = await _context.Usuarios.FindAsync(id);
            if (modelo == null)
            {
                return new ActionResponse<Usuario>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            return new ActionResponse<Usuario>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<Usuario>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Usuario>> UpdateAsync(Usuario modelo, string urlFront)
    {
        await _transactionManager.BeginTransactionAsync();

        try
        {
            modelo.FullName = $"{modelo.FirstName} {modelo.LastName}";
            if (!string.IsNullOrEmpty(modelo.ImgBase64))
            {
                modelo.FullName = $"{modelo.FirstName} {modelo.LastName}";
                string guid;
                if (modelo.Photo == null)
                {
                    guid = Guid.NewGuid().ToString() + ".jpg";
                }
                else
                {
                    guid = modelo.Photo;
                }
                var imageId = Convert.FromBase64String(modelo.ImgBase64);
                modelo.Photo = await _fileStorage.UploadImage(imageId, _imgOption.ImgUsuario, guid);
            }
            _context.Usuarios.Update(modelo);
            await _transactionManager.SaveChangesAsync();

            User UserCurrent = await _userHelper.GetUserAsync(modelo.UserName);
            if (UserCurrent != null)
            {
                UserCurrent.FirstName = modelo.FirstName;
                UserCurrent.LastName = modelo.LastName;
                UserCurrent.FullName = $"{modelo.FirstName} {modelo.LastName}";
                UserCurrent.PhoneNumber = modelo.PhoneNumber;
                UserCurrent.PhotoUser = modelo.Photo;
                UserCurrent.JobPosition = modelo.Job!;
                UserCurrent.Active = modelo.Active;
                IdentityResult result = await _userHelper.UpdateUserAsync(UserCurrent);
            }
            else
            {
                if (modelo.Active)
                {
                    Response response = await AcivateUser(modelo, urlFront);
                    if (response.IsSuccess == false)
                    {
                        var guid = modelo.Photo;
                        _fileStorage.DeleteImage(_imgOption.ImgUsuario, guid!);
                        await _transactionManager.RollbackTransactionAsync();
                        return new ActionResponse<Usuario>
                        {
                            WasSuccess = true,
                            Message = _localizer["Generic_UserCreationFail"]
                        };
                    }
                }
            }

            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Usuario>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Usuario>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Usuario>> AddAsync(Usuario modelo, string urlFront, string Email)
    {
        User user = await _userHelper.GetUserAsync(Email);
        if (user == null)
        {
            return new ActionResponse<Usuario>
            {
                WasSuccess = false,
                Message = _localizer["Generic_AuthIdFail"]
            };
        }
        User userCheck = await _userHelper.GetUserAsync(modelo.UserName);
        if (userCheck != null)
        {
            return new ActionResponse<Usuario>
            {
                WasSuccess = false,
                Message = _localizer["Generic_EmailAlreadyUsed"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            modelo.CorporationId = Convert.ToInt32(user.CorporationId);
            modelo.FullName = $"{modelo.FirstName} {modelo.LastName}";
            if (modelo.ImgBase64 is not null)
            {
                string guid = Guid.NewGuid().ToString() + ".jpg";
                var imageId = Convert.FromBase64String(modelo.ImgBase64);
                modelo.Photo = await _fileStorage.UploadImage(imageId, _imgOption.ImgUsuario, guid);
            }
            _context.Usuarios.Add(modelo);
            await _transactionManager.SaveChangesAsync();

            //Aseguramos los cambios en la base de datos
            if (modelo.Active)
            {
                Response response = await AcivateUser(modelo, urlFront);
                if (response.IsSuccess == false)
                {
                    var guid = modelo.Photo;
                    _fileStorage.DeleteImage(_imgOption.ImgUsuario, guid!);
                    await _transactionManager.RollbackTransactionAsync();
                    return new ActionResponse<Usuario>
                    {
                        WasSuccess = true,
                        Message = _localizer["Generic_UserCreationFail"]
                    };
                }
            }

            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Usuario>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Usuario>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(int id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.Usuarios.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.Usuarios.Remove(DataRemove);
            await _transactionManager.SaveChangesAsync();

            if (DataRemove.Photo is not null)
            {
                var response = _fileStorage.DeleteImage(_imgOption.ImgUsuario, DataRemove.Photo);
                if (!response)
                {
                    return new ActionResponse<bool>
                    {
                        WasSuccess = true,
                        Message = _localizer["Generic_RecordDeletedNoImage"]
                    };
                }
            }
            await _userHelper.DeleteUser(DataRemove.UserName);

            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<bool>
            {
                WasSuccess = true,
                Result = true
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<bool>(ex); // ✅ Manejo de errores automático
        }
    }

    private async Task<Response> AcivateUser(Usuario modelo, string urlFront)
    {
        User user = await _userHelper.AddUserUsuarioSoftAsync(modelo.FirstName, modelo.LastName, modelo.UserName,
            modelo.PhoneNumber!, modelo.Address!, modelo.Job!, modelo.CorporationId, modelo.Photo!, "UsuarioSoftware", modelo.Active);
        //Envio de Correo con Token de seguridad para Verificar el correo
        string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
        // Construir la URL sin `Url.Action`
        string tokenLink = $"{urlFront}/api/accounts/ConfirmEmail?userid={user.Id}&token={myToken}";

        string subject = "Activacion de Cuenta";
        string body = ($"De: NexxtPlanet" +
            $"<h1>Email Confirmation</h1>" +
            $"<p>" +
            $"Su Clave Temporal es: <h2> \"{user.Pass}\"</h2>" +
            $"</p>" +
            $"Para Activar su vuenta, " +
            $"Has Click en el siguiente Link:</br></br><strong><a href = \"{tokenLink}\">Confirmar Correo</a></strong>");

        Response response = await _emailHelper.ConfirmarCuenta(user.UserName!, user.FullName!, subject, body);
        if (response.IsSuccess == false)
        {
            return response;
        }
        return response;
    }
}