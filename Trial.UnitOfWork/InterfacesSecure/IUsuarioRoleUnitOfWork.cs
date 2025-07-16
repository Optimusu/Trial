using Trial.Domain.EntitesSoftSec;
using Trial.Domain.Enum;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.InterfacesSecure;

public interface IUsuarioRoleUnitOfWork
{
    Task<ActionResponse<IEnumerable<EnumItemModel>>> ComboAsync();

    Task<ActionResponse<IEnumerable<UsuarioRole>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<UsuarioRole>> GetAsync(int id);

    Task<ActionResponse<UsuarioRole>> AddAsync(UsuarioRole modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}