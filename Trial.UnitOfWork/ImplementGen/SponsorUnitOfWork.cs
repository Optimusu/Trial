using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesGen;
using Trial.UnitOfWork.InterfacesGen;

namespace Trial.UnitOfWork.ImplementGen;

public class SponsorUnitOfWork : ISponsorUnitOfWork
{
    private readonly ISponsorService _sponsorService;

    public SponsorUnitOfWork(ISponsorService sponsorService)
    {
        _sponsorService = sponsorService;
    }

    public async Task<ActionResponse<IEnumerable<Sponsor>>> ComboAsync() => await _sponsorService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Sponsor>>> GetAsync(PaginationDTO pagination) => await _sponsorService.GetAsync(pagination);

    public async Task<ActionResponse<Sponsor>> GetAsync(int id) => await _sponsorService.GetAsync(id);

    public async Task<ActionResponse<Sponsor>> UpdateAsync(Sponsor modelo) => await _sponsorService.UpdateAsync(modelo);

    public async Task<ActionResponse<Sponsor>> AddAsync(Sponsor modelo) => await _sponsorService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _sponsorService.DeleteAsync(id);
}