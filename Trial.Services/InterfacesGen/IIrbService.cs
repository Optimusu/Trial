using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesGen;

public interface IIrbService
{
    Task<ActionResponse<IEnumerable<Irb>>> ComboAsync(string Email);

    Task<ActionResponse<IEnumerable<Irb>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Irb>> GetAsync(int id);

    Task<ActionResponse<Irb>> UpdateAsync(Irb modelo);

    Task<ActionResponse<Irb>> AddAsync(Irb modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}