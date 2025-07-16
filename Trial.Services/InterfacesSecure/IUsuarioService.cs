using Trial.Domain.EntitesSoftSec;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesSecure;

public interface IUsuarioService
{
    Task<ActionResponse<IEnumerable<Usuario>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<Usuario>> GetAsync(int id);

    Task<ActionResponse<Usuario>> UpdateAsync(Usuario modelo, string UrlFront);

    Task<ActionResponse<Usuario>> AddAsync(Usuario modelo, string urlFront, string Email);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}