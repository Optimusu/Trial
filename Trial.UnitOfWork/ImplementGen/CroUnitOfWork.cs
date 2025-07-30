using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesGen;
using Trial.UnitOfWork.InterfacesGen;

namespace Trial.UnitOfWork.ImplementGen;

public class CroUnitOfWork : ICroUnitOfWork
{
    private readonly ICroService _croService;

    public CroUnitOfWork(ICroService croService)
    {
        _croService = croService;
    }

    public async Task<ActionResponse<IEnumerable<Cro>>> ComboAsync(string Email) => await _croService.ComboAsync(Email);

    public async Task<ActionResponse<IEnumerable<Cro>>> GetAsync(PaginationDTO pagination, string Email) => await _croService.GetAsync(pagination, Email);

    public async Task<ActionResponse<Cro>> GetAsync(int id) => await _croService.GetAsync(id);

    public async Task<ActionResponse<Cro>> UpdateAsync(Cro modelo) => await _croService.UpdateAsync(modelo);

    public async Task<ActionResponse<Cro>> AddAsync(Cro modelo, string Email) => await _croService.AddAsync(modelo, Email);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _croService.DeleteAsync(id);
}