using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesGen;
using Trial.UnitOfWork.InterfacesGen;

namespace Trial.UnitOfWork.ImplementGen;

public class IrbUnitOfWork : IIrbUnitOfWork
{
    private readonly IIrbService _irbService;

    public IrbUnitOfWork(IIrbService irbService)
    {
        _irbService = irbService;
    }

    public async Task<ActionResponse<IEnumerable<Irb>>> ComboAsync() => await _irbService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Irb>>> GetAsync(PaginationDTO pagination) => await _irbService.GetAsync(pagination);

    public async Task<ActionResponse<Irb>> GetAsync(int id) => await _irbService.GetAsync(id);

    public async Task<ActionResponse<Irb>> UpdateAsync(Irb modelo) => await _irbService.UpdateAsync(modelo);

    public async Task<ActionResponse<Irb>> AddAsync(Irb modelo) => await _irbService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _irbService.DeleteAsync(id);
}