using Trial.Domain.EntitiesStudy;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.InterfacesStudy;

public interface IEdocStudyUnitOfWork
{
    Task<ActionResponse<IEnumerable<EdocStudy>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<EdocStudy>> GetAsync(Guid id);

    Task<ActionResponse<EdocStudy>> UpdateAsync(EdocStudy modelo);

    Task<ActionResponse<EdocStudy>> AddAsync(EdocStudy modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}