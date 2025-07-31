using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.InterfacesGen;

public interface IIrbUnitOfWork
{
    Task<ActionResponse<IEnumerable<Irb>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Irb>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Irb>> GetAsync(int id);

    Task<ActionResponse<Irb>> UpdateAsync(Irb modelo);

    Task<ActionResponse<Irb>> AddAsync(Irb modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}