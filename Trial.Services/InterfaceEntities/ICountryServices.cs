using Trial.Domain.Entities;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfaceEntities;

public interface ICountryServices
{
    Task<ActionResponse<IEnumerable<Country>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<Country>> UpdateAsync(Country modelo);

    Task<ActionResponse<Country>> AddAsync(Country modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}