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

    public async Task<ActionResponse<IEnumerable<Sponsor>>> ComboAsync(string Email) => await _sponsorService.ComboAsync(Email);

    public async Task<ActionResponse<IEnumerable<Sponsor>>> GetAsync(PaginationDTO pagination, string Email) => await _sponsorService.GetAsync(pagination, Email);

    public async Task<ActionResponse<Sponsor>> GetAsync(int id) => await _sponsorService.GetAsync(id);

    public async Task<ActionResponse<Sponsor>> UpdateAsync(Sponsor modelo) => await _sponsorService.UpdateAsync(modelo);

    public async Task<ActionResponse<Sponsor>> AddAsync(Sponsor modelo, string Email) => await _sponsorService.AddAsync(modelo, Email);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _sponsorService.DeleteAsync(id);
}