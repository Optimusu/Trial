using Trial.Domain.Entities;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfaceEntities;
using Trial.UnitOfWork.InterfaceEntities;

namespace Trial.UnitOfWork.ImplementEntities;

public class CityUnitOfWork : ICityUnitOfWork
{
    private readonly ICityService _cityService;

    public CityUnitOfWork(ICityService cityService)
    {
        _cityService = cityService;
    }

    public async Task<ActionResponse<IEnumerable<City>>> ComboAsync(int id) => await _cityService.ComboAsync(id);

    public async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination) => await _cityService.GetAsync(pagination);

    public async Task<ActionResponse<City>> GetAsync(int id) => await _cityService.GetAsync(id);

    public async Task<ActionResponse<City>> UpdateAsync(City modelo) => await _cityService.UpdateAsync(modelo);

    public async Task<ActionResponse<City>> AddAsync(City modelo) => await _cityService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _cityService.DeleteAsync(id);
}