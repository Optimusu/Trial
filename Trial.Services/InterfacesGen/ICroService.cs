using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesGen;

public interface ICroService
{
    Task<ActionResponse<IEnumerable<Cro>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Cro>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Cro>> GetAsync(int id);

    Task<ActionResponse<Cro>> UpdateAsync(Cro modelo);

    Task<ActionResponse<Cro>> AddAsync(Cro modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}