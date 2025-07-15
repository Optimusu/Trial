using Trial.Domain.Entities;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfaceEntities;
using Trial.UnitOfWork.InterfaceEntities;

namespace Trial.UnitOfWork.ImplementEntities;

public class StateUnitOfWork : IStateUnitOfWork
{
    private readonly IStateService _stateService;

    public StateUnitOfWork(IStateService stateService)
    {
        _stateService = stateService;
    }

    public async Task<ActionResponse<IEnumerable<State>>> ComboAsync(ClaimsDTOs claimsDTOs) => await _stateService.ComboAsync(claimsDTOs);

    public async Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination) => await _stateService.GetAsync(pagination);

    public async Task<ActionResponse<State>> GetAsync(int id) => await _stateService.GetAsync(id);

    public async Task<ActionResponse<State>> UpdateAsync(State modelo) => await _stateService.UpdateAsync(modelo);

    public async Task<ActionResponse<State>> AddAsync(State modelo) => await _stateService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _stateService.DeleteAsync(id);
}