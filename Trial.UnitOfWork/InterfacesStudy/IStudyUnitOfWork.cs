using Trial.Domain.EntitiesStudy;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.InterfacesStudy;

public interface IStudyUnitOfWork
{
    Task<ActionResponse<IEnumerable<Study>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Study>> GetAsync(Guid id);

    Task<ActionResponse<Study>> UpdateAsync(Study modelo);

    Task<ActionResponse<Study>> AddAsync(Study modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}