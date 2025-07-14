using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.BaseImplement;

public class BaseUnitOfWork<TModel, TService> where TModel : class
{
    protected readonly TService _service;

    protected BaseUnitOfWork(TService service)
    {
        _service = service;
    }

    public virtual Task<ActionResponse<IEnumerable<TModel>>> GetAsync(PaginationDTO pagination, UserClaimsInfo? userClaimsInfo = null) =>
        ((dynamic)_service!).GetAsync(pagination, userClaimsInfo);

    public virtual Task<ActionResponse<TModel>> GetAsync(int id, UserClaimsInfo? userClaimsInfo = null) =>
        ((dynamic)_service!).GetAsync(id, userClaimsInfo);

    public virtual Task<ActionResponse<TModel>> AddAsync(TModel model, UserClaimsInfo? userClaimsInfo = null) =>
        ((dynamic)_service!).AddAsync(model, userClaimsInfo);

    public virtual Task<ActionResponse<TModel>> UpdateAsync(TModel model, UserClaimsInfo? userClaimsInfo = null) =>
        ((dynamic)_service!).UpdateAsync(model, userClaimsInfo);

    public virtual Task<ActionResponse<bool>> DeleteAsync(int id, UserClaimsInfo? userClaimsInfo = null) =>
        ((dynamic)_service!).DeleteAsync(id, userClaimsInfo);
}