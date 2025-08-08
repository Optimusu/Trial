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

public class StudyService : IStudyService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IStringLocalizer _localizer;
    private readonly IUserHelper _userHelper;
    private readonly IMapperService _mapperService;

    public StudyService(DataContext context, IHttpContextAccessor httpContextAccessor,
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

    public async Task<ActionResponse<IEnumerable<EnumItemModel>>> ComboAsync()
    {
        try
        {
            List<EnumItemModel> list = Enum.GetValues(typeof(TrialPhase)).Cast<TrialPhase>().Select(c => new EnumItemModel()
            {
                Name = c.ToString(),
                Value = (int)c
            }).ToList();

            list.Insert(0, new EnumItemModel
            {
                Name = _localizer["[Select Phase]"],
                Value = 0
            });

            return new ActionResponse<IEnumerable<EnumItemModel>>
            {
                WasSuccess = true,
                Result = list
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<EnumItemModel>>(ex); //Manejo de errores automático
        }
    }

    public async Task<ActionResponse<IEnumerable<Study>>> GetAsync(PaginationDTO pagination, string Email)
    {
        try
        {
            User user = await _userHelper.GetUserAsync(Email);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<Study>>
                {
                    WasSuccess = false,
                    Message = "Problemas para Conseguir el Usuario"
                };
            }

            var queryable = _context.Studies
                .Include(x => x.EdocCategories)
                .Where(x => x.CorporationId == user.CorporationId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                //Busqueda grandes mateniendo los indices de los campos, campo Esta Collation CI para Case Insensitive
                queryable = queryable.Where(u => EF.Functions.Like(u.Protocol, $"%{pagination.Filter}%"));
            }
            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.Protocol).Paginate(pagination).ToListAsync();

            return new ActionResponse<IEnumerable<Study>>
            {
                WasSuccess = true,
                Result = queryable
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<Study>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Study>> GetAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                return new ActionResponse<Study>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_InvalidId"]
                };
            }
            var modelo = await _context.Studies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudyId == id);
            if (modelo == null)
            {
                return new ActionResponse<Study>
                {
                    WasSuccess = false,
                    Message = "Problemas para Enconstrar el Registro Indicado"
                };
            }

            return new ActionResponse<Study>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<Study>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Study>> UpdateAsync(Study modelo)
    {
        if (modelo == null || modelo.StudyId == Guid.Empty)
        {
            return new ActionResponse<Study>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidId"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            //Para guardar el dato del sponsor
            var DatoSponsor = await _context.Sponsors.AsNoTracking().FirstOrDefaultAsync(x => x.SponsorId == modelo.SponsorId);
            modelo.FullStudy = $"{modelo.Protocol} - {DatoSponsor!.Name}";

            Study NuevoModelo = _mapperService.Map<Study, Study>(modelo);
            _context.Studies.Update(NuevoModelo);

            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Study>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Study>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<Study>> AddAsync(Study modelo, string Email)
    {
        User user = await _userHelper.GetUserAsync(Email);
        if (user == null)
        {
            return new ActionResponse<Study>
            {
                WasSuccess = false,
                Message = _localizer["Generic_AuthIdFail"]
            };
        }
        //Para guardar el dato del sponsor
        var DatoSponsor = await _context.Sponsors.AsNoTracking().FirstOrDefaultAsync(x => x.SponsorId == modelo.SponsorId);
        modelo.FullStudy = $"{modelo.Protocol} - {DatoSponsor!.Name}";

        modelo.CorporationId = Convert.ToInt32(user.CorporationId);

        if (!ValidatorModel.IsValid(modelo, out var errores))
        {
            return new ActionResponse<Study>
            {
                WasSuccess = false,
                Message = _localizer["Generic_InvalidModel"] // 🧠 Clave multilenguaje para modelo nulo
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            Study NuevoModelo = _mapperService.Map<Study, Study>(modelo);
            _context.Studies.Add(NuevoModelo);
            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<Study>
            {
                WasSuccess = true,
                Result = modelo,
                Message = _localizer["Generic_Success"]
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<Study>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.Studies.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.Studies.Remove(DataRemove);

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