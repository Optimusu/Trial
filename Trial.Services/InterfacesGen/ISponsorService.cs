using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesGen;

public interface ISponsorService
{
    Task<ActionResponse<IEnumerable<Sponsor>>> ComboAsync(string Email);

    Task<ActionResponse<IEnumerable<Sponsor>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Sponsor>> GetAsync(int id);

    Task<ActionResponse<Sponsor>> UpdateAsync(Sponsor modelo);

    Task<ActionResponse<Sponsor>> AddAsync(Sponsor modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}