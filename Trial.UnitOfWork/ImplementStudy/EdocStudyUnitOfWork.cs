using Trial.Domain.EntitiesStudy;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesStudy;
using Trial.UnitOfWork.InterfacesStudy;

namespace Trial.UnitOfWork.ImplementStudy;

public class EdocStudyUnitOfWork : IEdocStudyUnitOfWork
{
    private readonly IEdocStudyService _edocStudy;

    public EdocStudyUnitOfWork(IEdocStudyService edocStudy)
    {
        _edocStudy = edocStudy;
    }

    public async Task<ActionResponse<IEnumerable<EdocStudy>>> GetAsync(PaginationDTO pagination, string Email) => await _edocStudy.GetAsync(pagination, Email);

    public async Task<ActionResponse<EdocStudy>> GetAsync(Guid id) => await _edocStudy.GetAsync(id);

    public async Task<ActionResponse<EdocStudy>> UpdateAsync(EdocStudy modelo) => await _edocStudy.UpdateAsync(modelo);

    public async Task<ActionResponse<EdocStudy>> AddAsync(EdocStudy modelo, string Email) => await _edocStudy.AddAsync(modelo, Email);

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _edocStudy.DeleteAsync(id);
}