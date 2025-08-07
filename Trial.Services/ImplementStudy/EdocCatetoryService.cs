using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Trial.AppInfra;
using Trial.AppInfra.ErrorHandling;
using Trial.AppInfra.Extensions;
using Trial.AppInfra.Mappings;
using Trial.AppInfra.Transactions;
using Trial.AppInfra.UserHelper;
using Trial.AppInfra.Validations;
using Trial.Domain.Entities;
using Trial.Domain.EntitiesStudy;
using Trial.Domain.Enum;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesStudy;

namespace Trial.Services.ImplementStudy;

public class EdocCatetoryService : IEdocCatetoryService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IStringLocalizer _localizer;
    private readonly IUserHelper _userHelper;
    private readonly IMapperService _mapperService;

    public EdocCatetoryService(DataContext context, IHttpContextAccessor httpContextAccessor,
        ITransactionManager transactionManager, HttpErrorHandler httpErrorHandler,
        IStringLocalizer localizer, IUserHelper userHelper, IMapperService mapperService)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _transactionManager = transactionManager;
        _httpErrorHandler = httpErrorHandler;
        _localizer = localizer;
        _userHelper = userHelper;
        _mapperService = mapperService;
    }

    public async Task<ActionResponse<IEnumerable<GuidItemModel>>> ComboAsync(string email)
    {
        try
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<GuidItemModel>>
                {
                    WasSuccess = false,
                    Message = "Problemas para Conseguir el Usuario"
                };
            }

            // Supongamos que tienes acceso a un DbContext o servicio que devuelve las categorías
            var categories = await _context.EdocCategories.Where(x => x.Active && x.CorporationId == user.CorporationId)
                .Select(c => new GuidItemModel
                {
                    Name = c.Name, // o el campo que represente el nombre visible
                    Value = c.EdocCategoryId
                })
                .ToListAsync();

            // Insertar opción por defecto al inicio
            categories.Insert(0, new GuidItemModel
            {
                Name = _localizer["[Select Category]"],
                Value = Guid.Empty
            });

            return new ActionResponse<IEnumerable<GuidItemModel>>
            {
                WasSuccess = true,
                Result = categories
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<GuidItemModel>>(ex);
        }
    }

    public async Task<ActionResponse<IEnumerable<EdocCategory>>> GetAsync(PaginationDTO pagination, string Email)
    {
        try
        {
            User user = await _userHelper.GetUserAsync(Email);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<EdocCategory>>
                {
                    WasSuccess = false,
                    Message = "Problemas para Conseguir el Usuario"
                };
            }

            var queryable = _context.EdocCategories.Where(x => x.CorporationId == user.CorporationId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                //Busqueda grandes mateniendo los indices de los campos, campo Esta Collation CI para Case Insensitive
                queryable = queryable.Where(u => EF.Functions.Like(u.Name, $"%{pagination.Filter}%"));
            }
            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<EdocCategory>>
            {
                WasSuccess = true,
                Result = queryable
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<EdocCategory>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<EdocCategory>> GetAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                return new ActionResponse<EdocCategory>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_InvalidId"]
                };
            }
            var modelo = await _context.EdocCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudyId == id);
            if (modelo == null)
            {
                return new ActionResponse<EdocCategory>
                {
                    WasSuccess = false,
                    Message = "Problemas para Enconstrar el Registro Indicado"
                };
            }

            return new ActionResponse<EdocCategory>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<EdocCategory>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<EdocCategory>> UpdateAsync(EdocCategory modelo)
    {
        if (modelo == null || modelo.EdocCategoryId == Guid.Empty)
        {
            return new ActionResponse<EdocCategory>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidId"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.EdocCategories.Update(modelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<EdocCategory>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<EdocCategory>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<EdocCategory>> AddAsync(EdocCategory modelo, string Email)
    {
        User user = await _userHelper.GetUserAsync(Email);
        if (user == null)
        {
            return new ActionResponse<EdocCategory>
            {
                WasSuccess = false,
                Message = _localizer["Generic_AuthIdFail"]
            };
        }

        modelo.CorporationId = Convert.ToInt32(user.CorporationId);

        if (!ValidatorModel.IsValid(modelo, out var errores))
        {
            return new ActionResponse<EdocCategory>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidModel"] // 🧠 Clave multilenguaje para modelo nulo
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            _context.EdocCategories.Add(modelo);
            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<EdocCategory>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<EdocCategory>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.EdocCategories.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.EdocCategories.Remove(DataRemove);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<bool>
            {
                WasSuccess = true,
                Result = true,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<bool>(ex); // ✅ Manejo de errores automático
        }
    }
}