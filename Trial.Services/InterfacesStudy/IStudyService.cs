using Trial.Domain.EntitiesStudy;
using Trial.Domain.Enum;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesStudy;

public interface IStudyService
{
    Task<ActionResponse<IEnumerable<EnumItemModel>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Study>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Study>> GetAsync(Guid id);

    Task<ActionResponse<Study>> UpdateAsync(Study modelo);

    Task<ActionResponse<Study>> AddAsync(Study modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}