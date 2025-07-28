﻿using Microsoft.AspNetCore.Http;
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
using Trial.AppInfra.Mappings;
using Trial.AppInfra.Transactions;
using Trial.AppInfra.UserHelper;
using Trial.Domain.Entities;
using Trial.Domain.Enum;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfaceEntities;

namespace Trial.Services.ImplementEntties;

public class ManagerService : IManagerService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly IFileStorage _fileStorage;
    private readonly IUserHelper _userHelper;
    private readonly IEmailHelper _emailHelper;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IStringLocalizer _localizer;
    private readonly IMapperService _mapperService;
    private readonly ImgSetting _imgOption;

    public ManagerService(DataContext context, IHttpContextAccessor httpContextAccessor,
        ITransactionManager transactionManager, IMemoryCache cache, IFileStorage fileStorage,
        IUserHelper userHelper, IEmailHelper emailHelper, IOptions<ImgSetting> ImgOption,
        HttpErrorHandler httpErrorHandler, IStringLocalizer localizer, IMapperService mapperService)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _transactionManager = transactionManager;
        _fileStorage = fileStorage;
        _userHelper = userHelper;
        _emailHelper = emailHelper;
        _httpErrorHandler = httpErrorHandler;
        _localizer = localizer;
        _mapperService = mapperService;
        _imgOption = ImgOption.Value;
    }

    public async Task<ActionResponse<IEnumerable<Manager>>> GetAsync(PaginationDTO pagination)
    {
        try
        {
            var queryable = _context.Managers.Include(x => x.Corporation).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                //Busqueda grandes mateniendo los indices de los campos, campo Esta Collation CI para Case Insensitive
                queryable = queryable.Where(u => EF.Functions.Like(u.FullName, $"%{pagination.Filter}%"));
            }

            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.FullName).Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<Manager>>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Manager>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Manager>> GetAsync(int id)
    {
        try
        {
            if (id <= 0)
            {
                return new ActionResponse<Manager>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_InvalidId"]
                };
            }
            var modelo = await _context.Managers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.ManagerId == id);
            if (modelo == null)
            {
                return new ActionResponse<Manager>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            return new ActionResponse<Manager>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<Manager>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Manager>> UpdateAsync(Manager modelo, string frontUrl)
    {
        await _transactionManager.BeginTransactionAsync();

        try
        {
            Manager NewModelo = new()
            {
                ManagerId = modelo.ManagerId,
                FirstName = modelo.FirstName,
                LastName = modelo.LastName,
                TypeDocument = modelo.TypeDocument,
                FullName = $"{modelo.FirstName} {modelo.LastName}",
                NroDocument = modelo.NroDocument,
                PhoneNumber = modelo.PhoneNumber,
                Address = modelo.Address,
                UserName = modelo.UserName,
                CorporationId = modelo.CorporationId,
                Job = modelo.Job,
                UserType = modelo.UserType,
                Imagen = modelo.Imagen,
                Active = modelo.Active,
            };
            if (modelo.ImgBase64 != null)
            {
                NewModelo.ImgBase64 = modelo.ImgBase64;
            }

            if (!string.IsNullOrEmpty(modelo.ImgBase64))
            {
                string guid;
                if (modelo.Imagen == null)
                {
                    guid = Guid.NewGuid().ToString() + ".jpg";
                }
                else
                {
                    guid = modelo.Imagen;
                }
                var imageId = Convert.FromBase64String(modelo.ImgBase64);
                NewModelo.Imagen = await _fileStorage.UploadImage(imageId, _imgOption.ImgManager!, guid);
            }
            _context.Managers.Update(NewModelo);
            await _transactionManager.SaveChangesAsync();

            User UserCurrent = await _userHelper.GetUserAsync(modelo.UserName);
            if (UserCurrent != null)
            {
                UserCurrent.FirstName = modelo.FirstName;
                UserCurrent.LastName = modelo.LastName;
                UserCurrent.FullName = $"{modelo.FirstName} {modelo.LastName}";
                UserCurrent.PhoneNumber = modelo.PhoneNumber;
                UserCurrent.PhotoUser = modelo.Imagen;
                UserCurrent.JobPosition = modelo.Job;
                UserCurrent.Active = modelo.Active;
                IdentityResult result = await _userHelper.UpdateUserAsync(UserCurrent);
            }
            else
            {
                if (modelo.Active)
                {
                    Response response = await AcivateUser(modelo, frontUrl);
                    if (response.IsSuccess == false)
                    {
                        var guid = modelo.Imagen;
                        _fileStorage.DeleteImage(_imgOption.ImgManager!, guid!);
                        await _transactionManager.RollbackTransactionAsync();
                        return new ActionResponse<Manager>
                        {
                            WasSuccess = true,
                            Message = _localizer["Generic_UserCreationFail"]
                        };
                    }
                }
            }

            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Manager>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Manager>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Manager>> AddAsync(Manager Newmodelo, string frontUrl)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            Manager modelo = new()
            {
                ManagerId = Newmodelo.ManagerId,
                FirstName = Newmodelo.FirstName,
                LastName = Newmodelo.LastName,
                TypeDocument = Newmodelo.TypeDocument,
                FullName = $"{Newmodelo.FirstName} {Newmodelo.LastName}",
                NroDocument = Newmodelo.NroDocument,
                PhoneNumber = Newmodelo.PhoneNumber,
                Address = Newmodelo.Address,
                UserName = Newmodelo.UserName,
                CorporationId = Newmodelo.CorporationId,
                Job = Newmodelo.Job,
                UserType = UserType.Administrator,  //Tipo de UserRole
                Imagen = Newmodelo.Imagen,
                Active = Newmodelo.Active,
            };
            if (Newmodelo.ImgBase64 != null)
            {
                modelo.ImgBase64 = Newmodelo.ImgBase64;
            }

            User CheckEmail = await _userHelper.GetUserAsync(modelo.UserName);
            if (CheckEmail != null)
            {
                return new ActionResponse<Manager>
                {
                    WasSuccess = true,
                    Message = _localizer["Generic_EmailAlreadyUsed"]
                };
            }

            if (!string.IsNullOrEmpty(modelo.ImgBase64))
            {
                string guid = Guid.NewGuid().ToString() + ".jpg";
                var imageId = Convert.FromBase64String(modelo.ImgBase64);
                modelo.Imagen = await _fileStorage.UploadImage(imageId, _imgOption.ImgManager!, guid);
            }

            _context.Managers.Add(modelo);
            await _transactionManager.SaveChangesAsync();

            //Registro del Usuario en User
            if (modelo.Active)
            {
                Response response = await AcivateUser(modelo, frontUrl);
                if (!response.IsSuccess)
                {
                    var guid = modelo.Imagen;
                    _fileStorage.DeleteImage(_imgOption.ImgManager!, guid!);
                    await _transactionManager.RollbackTransactionAsync();
                    return new ActionResponse<Manager>
                    {
                        WasSuccess = true,
                        Message = _localizer["Generic_UserCreationFail"]
                    };
                }
            }

            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Manager>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Manager>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(int id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.Managers.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }
            var user = await _userHelper.GetUserAsync(DataRemove.UserName);
            var RemoveRoleDetail = await _context.UserRoleDetails.Where(x => x.UserId == user.Id).ToListAsync();
            if (RemoveRoleDetail != null)
            {
                _context.UserRoleDetails.RemoveRange(RemoveRoleDetail!);
            }
            await _userHelper.DeleteUser(DataRemove.UserName);

            _context.Managers.Remove(DataRemove);

            if (DataRemove.Imagen is not null)
            {
                var response = _fileStorage.DeleteImage(_imgOption.ImgManager!, DataRemove.Imagen);
                if (!response)
                {
                    return new ActionResponse<bool>
                    {
                        WasSuccess = false,
                        Message = _localizer["Generic_RecordDeletedNoImage"]
                    };
                }
            }

            await _transactionManager.SaveChangesAsync();
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

    private async Task<Response> AcivateUser(Manager manager, string frontUrl)
    {
        User user = await _userHelper.AddUserUsuarioAsync(manager.FirstName, manager.LastName, manager.UserName,
            manager.PhoneNumber, manager.Address, manager.Job, manager.CorporationId, manager.Imagen!, "Manager", manager.Active, manager.UserType);

        //Envio de Correo con Token de seguridad para Verificar el correo
        string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

        // Construir la URL sin `Url.Action`
        string tokenLink = $"{frontUrl}/api/accounts/ConfirmEmail?userid={user.Id}&token={myToken}";

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