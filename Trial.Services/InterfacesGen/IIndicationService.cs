using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesGen;

public interface IIndicationService
{
    Task<ActionResponse<IEnumerable<Indication>>> ComboAsync(string Email);

    Task<ActionResponse<IEnumerable<Indication>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Indication>> GetAsync(int id);

    Task<ActionResponse<Indication>> UpdateAsync(Indication modelo);

    Task<ActionResponse<Indication>> AddAsync(Indication modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}