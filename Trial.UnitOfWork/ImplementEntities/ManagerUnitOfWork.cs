using Trial.Domain.Entities;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfaceEntities;
using Trial.UnitOfWork.InterfaceEntities;

namespace Trial.UnitOfWork.ImplementEntities;

public class ManagerUnitOfWork : IManagerUnitOfWork
{
    private readonly IManagerService _managerService;

    public ManagerUnitOfWork(IManagerService managerService)
    {
        _managerService = managerService;
    }

    public async Task<ActionResponse<IEnumerable<Manager>>> GetAsync(PaginationDTO pagination) => await _managerService.GetAsync(pagination);

    public async Task<ActionResponse<Manager>> GetAsync(int id) => await _managerService.GetAsync(id);

    public async Task<ActionResponse<Manager>> UpdateAsync(Manager modelo, string frontUrl) => await _managerService.UpdateAsync(modelo, frontUrl);

    public async Task<ActionResponse<Manager>> AddAsync(Manager modelo, string frontUrl) => await _managerService.AddAsync(modelo, frontUrl);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _managerService.DeleteAsync(id);
}