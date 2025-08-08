using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Trial.AppInfra;
using Trial.AppInfra.EmailHelper;
using Trial.AppInfra.ErrorHandling;
using Trial.AppInfra.Extensions;
using Trial.AppInfra.FileHelper;
using Trial.AppInfra.Transactions;
using Trial.AppInfra.UserHelper;
using Trial.Domain.Entities;
using Trial.Domain.EntitiesStudy;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesStudy;

namespace Trial.Services.ImplementStudy;

public class EdocStudyService : IEdocStudyService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITransactionManager _transactionManager;
    private readonly HttpErrorHandler _httpErrorHandler;
    private readonly IFileStorage _fileStorage;
    private readonly IUserHelper _userHelper;
    private readonly ImgSetting _imgOption;
    private readonly IStringLocalizer _localizer;

    public EdocStudyService(DataContext context, IHttpContextAccessor httpContextAccessor, HttpErrorHandler httpErrorHandle,
        ITransactionManager transactionManager, IFileStorage fileStorage, IStringLocalizer localizer,
        IUserHelper userHelper, IOptions<ImgSetting> ImgOption)
    {
        _context = context;
        _httpErrorHandler = httpErrorHandle;
        _httpContextAccessor = httpContextAccessor;
        _transactionManager = transactionManager;
        _fileStorage = fileStorage;
        _userHelper = userHelper;
        _imgOption = ImgOption.Value;
        _localizer = localizer;
    }

    public async Task<ActionResponse<IEnumerable<EdocStudy>>> GetAsync(PaginationDTO pagination, string Email)
    {
        try
        {
            User user = await _userHelper.GetUserAsync(Email);
            if (user == null)
            {
                return new ActionResponse<IEnumerable<EdocStudy>>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_AuthIdFail"]
                };
            }
            var DocCategory = await _context.EdocCategories.FirstOrDefaultAsync(x => x.EdocCategoryId == pagination.GuidId);
            if (DocCategory == null)
            {
                return new ActionResponse<IEnumerable<EdocStudy>>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_AuthIdFail"]
                };
            }
            var queryable = _context.EdocStudies
                .Where(x => x.CorporationId == user.CorporationId && x.EdocCategoryId == DocCategory!.EdocCategoryId).AsQueryable();

            //if (!string.IsNullOrWhiteSpace(pagination.Filter))
            //{
            //    queryable = queryable.Where(u => EF.Functions.Like(u.FullName, $"%{pagination.Filter}%"));
            //}
            await _httpContextAccessor.HttpContext!.InsertParameterPagination(queryable, pagination.RecordsNumber);
            var modelo = await queryable.OrderBy(x => x.DateCreated).Paginate(pagination).ToListAsync();

            await Task.WhenAll(modelo.Select(async option =>
            {
                if (string.IsNullOrWhiteSpace(option.File))
                {
                    option.FileFullPath = _imgOption.ImgNoImage; // imagen pública libre
                }
                else
                {
                    option.FileFullPath = await _fileStorage.GetImageBase64Async(
                        option.File,
                        DocCategory.NameContainer!);
                }
            }));

            return new ActionResponse<IEnumerable<EdocStudy>>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<IEnumerable<EdocStudy>>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<EdocStudy>> GetAsync(Guid id)
    {
        try
        {
            var modelo = await _context.EdocStudies.FindAsync(id);
            if (modelo == null)
            {
                return new ActionResponse<EdocStudy>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }
            var DocCategory = await _context.EdocCategories.FirstOrDefaultAsync(x => x.EdocCategoryId == modelo.EdocCategoryId);
            if (DocCategory == null)
            {
                return new ActionResponse<EdocStudy>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            if (string.IsNullOrWhiteSpace(modelo.File))
            {
                modelo.FileFullPath = _imgOption.ImgNoImage; // imagen pública libre
            }
            else
            {
                modelo.FileFullPath = await _fileStorage.GetImageBase64Async(
                    modelo.File,
                    DocCategory.NameContainer!);
            }

            return new ActionResponse<EdocStudy>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            return await _httpErrorHandler.HandleErrorAsync<EdocStudy>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<EdocStudy>> UpdateAsync(EdocStudy modelo)
    {
        await _transactionManager.BeginTransactionAsync();

        try
        {
            var DocCategory = await _context.EdocCategories.FirstOrDefaultAsync(x => x.EdocCategoryId == modelo.EdocCategoryId);
            if (DocCategory == null)
            {
                return new ActionResponse<EdocStudy>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }
            if (!string.IsNullOrEmpty(modelo.ImgBase64))
            {
                string guid;
                if (modelo.File == null)
                {
                    guid = Guid.NewGuid().ToString() + ".jpg";
                }
                else
                {
                    guid = modelo.File;
                }
                var imageId = Convert.FromBase64String(modelo.ImgBase64);
                //modelo.Photo = await _fileStorage.UploadImage(imageId, _imgOption.ImgUsuario, guid);
                modelo.File = await _fileStorage.SaveFileAsync(imageId, guid, DocCategory.NameContainer);
            }
            _context.EdocStudies.Update(modelo);
            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<EdocStudy>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<EdocStudy>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<EdocStudy>> AddAsync(EdocStudy modelo, string Email)
    {
        User user = await _userHelper.GetUserAsync(Email);
        if (user == null)
        {
            return new ActionResponse<EdocStudy>
            {
                WasSuccess = false,
                Message = _localizer["Generic_AuthIdFail"]
            };
        }
        var DocCategory = await _context.EdocCategories.FirstOrDefaultAsync(x => x.EdocCategoryId == modelo.EdocCategoryId);
        if (DocCategory == null)
        {
            return new ActionResponse<EdocStudy>
            {
                WasSuccess = false,
                Message = _localizer["Generic_IdNotFound"]
            };
        }

        await _transactionManager.BeginTransactionAsync();
        try
        {
            modelo.CorporationId = Convert.ToInt32(user.CorporationId);
            modelo.DateCreated = DateTime.Now;
            if (modelo.ImgBase64 is not null)
            {
                var extension = Path.GetExtension(modelo.FileNameOriginal);
                string guid = Guid.NewGuid().ToString() + extension;
                var imageId = Convert.FromBase64String(modelo.ImgBase64);
                //modelo.Photo = await _fileStorage.UploadImage(imageId, _imgOption.ImgUsuario, guid);
                modelo.File = await _fileStorage.SaveFileAsync(imageId, guid, DocCategory.NameContainer!);
            }
            _context.EdocStudies.Add(modelo);
            await _transactionManager.SaveChangesAsync();
            await _transactionManager.CommitTransactionAsync();

            return new ActionResponse<EdocStudy>
            {
                WasSuccess = true,
                Result = modelo
            };
        }
        catch (Exception ex)
        {
            await _transactionManager.RollbackTransactionAsync();
            return await _httpErrorHandler.HandleErrorAsync<EdocStudy>(ex); // ✅ Manejo de errores automático
        }
    }

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id)
    {
        await _transactionManager.BeginTransactionAsync();
        try
        {
            var DataRemove = await _context.EdocStudies.FindAsync(id);
            if (DataRemove == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }
            var DocCategory = await _context.EdocCategories.FirstOrDefaultAsync(x => x.EdocCategoryId == DataRemove.EdocCategoryId);
            if (DocCategory == null)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = _localizer["Generic_IdNotFound"]
                };
            }

            _context.EdocStudies.Remove(DataRemove);
            await _transactionManager.SaveChangesAsync();

            if (DataRemove.File is not null)
            {
                var response = _fileStorage.RemoveFileAsync(DataRemove.File, DocCategory.NameContainer!);
                if (!response.IsCompleted)
                {
                    return new ActionResponse<bool>
                    {
                        WasSuccess = true,
                        Message = _localizer["Generic_RecordDeletedNoImage"]
                    };
                }
            }
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
}