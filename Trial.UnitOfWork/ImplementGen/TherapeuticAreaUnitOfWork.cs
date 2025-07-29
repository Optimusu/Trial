using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesGen;
using Trial.UnitOfWork.InterfacesGen;

namespace Trial.UnitOfWork.ImplementGen;

public class TherapeuticAreaUnitOfWork : ITherapeuticAreaUnitOfWork
{
    private readonly ITherapeuticAreaService _therapeuticArea;

    public TherapeuticAreaUnitOfWork(ITherapeuticAreaService therapeuticArea)
    {
        _therapeuticArea = therapeuticArea;
    }

    public async Task<ActionResponse<IEnumerable<TherapeuticArea>>> ComboAsync() => await _therapeuticArea.ComboAsync();

    public async Task<ActionResponse<IEnumerable<TherapeuticArea>>> GetAsync(PaginationDTO pagination) => await _therapeuticArea.GetAsync(pagination);

    public async Task<ActionResponse<TherapeuticArea>> GetAsync(int id) => await _therapeuticArea.GetAsync(id);

    public async Task<ActionResponse<TherapeuticArea>> UpdateAsync(TherapeuticArea modelo) => await _therapeuticArea.UpdateAsync(modelo);

    public async Task<ActionResponse<TherapeuticArea>> AddAsync(TherapeuticArea modelo) => await _therapeuticArea.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _therapeuticArea.DeleteAsync(id);
}