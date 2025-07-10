using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.BaseImplement;

public class BaseUnitOfWork<TModel, TService> where TModel : class
{
    protected readonly TService _service;

    protected BaseUnitOfWork(TService service)
    {
        _service = service;
    }

    public virtual Task<ActionResponse<IEnumerable<TModel>>> GetAsync(PaginationDTO pagination) =>
        ((dynamic)_service!).GetAsync(pagination);

    public virtual Task<ActionResponse<TModel>> GetAsync(int id) =>
        ((dynamic)_service!).GetAsync(id);

    public virtual Task<ActionResponse<TModel>> AddAsync(TModel model) =>
        ((dynamic)_service!).AddAsync(model);

    public virtual Task<ActionResponse<TModel>> UpdateAsync(TModel model) =>
        ((dynamic)_service!).UpdateAsync(model);

    public virtual Task<ActionResponse<bool>> DeleteAsync(int id) =>
        ((dynamic)_service!).DeleteAsync(id);
}