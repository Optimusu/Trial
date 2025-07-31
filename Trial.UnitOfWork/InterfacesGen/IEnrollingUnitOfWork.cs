using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.InterfacesGen;

public interface IEnrollingUnitOfWork
{
    Task<ActionResponse<IEnumerable<Enrolling>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Enrolling>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Enrolling>> GetAsync(int id);

    Task<ActionResponse<Enrolling>> UpdateAsync(Enrolling modelo);

    Task<ActionResponse<Enrolling>> AddAsync(Enrolling modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}