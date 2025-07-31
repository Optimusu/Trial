using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesGen;

public interface ITherapeuticAreaService
{
    Task<ActionResponse<IEnumerable<TherapeuticArea>>> ComboAsync();

    Task<ActionResponse<IEnumerable<TherapeuticArea>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<TherapeuticArea>> GetAsync(int id);

    Task<ActionResponse<TherapeuticArea>> UpdateAsync(TherapeuticArea modelo);

    Task<ActionResponse<TherapeuticArea>> AddAsync(TherapeuticArea modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}