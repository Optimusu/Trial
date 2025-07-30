using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesGen;
using Trial.UnitOfWork.InterfacesGen;

namespace Trial.UnitOfWork.ImplementGen;

public class IndicationUnitOfWork : IIndicationUnitOfWork
{
    private readonly IIndicationService _indicationService;

    public IndicationUnitOfWork(IIndicationService indicationService)
    {
        _indicationService = indicationService;
    }

    public async Task<ActionResponse<IEnumerable<Indication>>> ComboAsync(string Email) => await _indicationService.ComboAsync(Email);

    public async Task<ActionResponse<IEnumerable<Indication>>> GetAsync(PaginationDTO pagination, string Email) => await _indicationService.GetAsync(pagination, Email);

    public async Task<ActionResponse<Indication>> GetAsync(int id) => await _indicationService.GetAsync(id);

    public async Task<ActionResponse<Indication>> UpdateAsync(Indication modelo) => await _indicationService.UpdateAsync(modelo);

    public async Task<ActionResponse<Indication>> AddAsync(Indication modelo, string Email) => await _indicationService.AddAsync(modelo, Email);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _indicationService.DeleteAsync(id);
}