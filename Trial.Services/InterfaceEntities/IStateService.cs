using Trial.Domain.Entities;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfaceEntities;

public interface IStateService
{
    Task<ActionResponse<IEnumerable<State>>> ComboAsync(ClaimsDTOs claimsDTO);

    Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<State>> GetAsync(int id);

    Task<ActionResponse<State>> UpdateAsync(State modelo);

    Task<ActionResponse<State>> AddAsync(State modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}