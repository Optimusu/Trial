using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesGen;

public interface ICroService
{
    Task<ActionResponse<IEnumerable<Cro>>> ComboAsync(string Email);

    Task<ActionResponse<IEnumerable<Cro>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Cro>> GetAsync(int id);

    Task<ActionResponse<Cro>> UpdateAsync(Cro modelo);

    Task<ActionResponse<Cro>> AddAsync(Cro modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}