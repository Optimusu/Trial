using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.BaseInterface;

public interface IBaseUnitOfWork<T> where T : class
{
    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination, UserClaimsInfo? userClaimsInfo = null);

    Task<ActionResponse<T>> GetAsync(int id, UserClaimsInfo? userClaimsInfo = null);

    Task<ActionResponse<T>> AddAsync(T model, UserClaimsInfo? userClaimsInfo = null);

    Task<ActionResponse<T>> UpdateAsync(T model, UserClaimsInfo? userClaimsInfo = null);

    Task<ActionResponse<bool>> DeleteAsync(int id, UserClaimsInfo? userClaimsInfo = null);
}