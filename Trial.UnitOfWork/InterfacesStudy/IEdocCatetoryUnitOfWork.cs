using Trial.Domain.EntitiesStudy;
using Trial.Domain.Enum;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.InterfacesStudy;

public interface IEdocCatetoryUnitOfWork
{
    Task<ActionResponse<IEnumerable<GuidItemModel>>> ComboAsync(string email);

    Task<ActionResponse<IEnumerable<EdocCategory>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<EdocCategory>> GetAsync(Guid id);

    Task<ActionResponse<EdocCategory>> UpdateAsync(EdocCategory modelo);

    Task<ActionResponse<EdocCategory>> AddAsync(EdocCategory modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(Guid id);
}