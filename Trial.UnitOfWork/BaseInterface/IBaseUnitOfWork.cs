using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.BaseInterface;

public interface IBaseUnitOfWork<T> where T : class
{
    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<T>> GetAsync(int id);

    Task<ActionResponse<T>> AddAsync(T model);

    Task<ActionResponse<T>> UpdateAsync(T model);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}