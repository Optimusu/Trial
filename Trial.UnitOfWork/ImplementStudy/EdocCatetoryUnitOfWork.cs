using Trial.Domain.EntitiesStudy;
using Trial.Domain.Enum;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesStudy;
using Trial.UnitOfWork.InterfacesStudy;

namespace Trial.UnitOfWork.ImplementStudy;

public class EdocCatetoryUnitOfWork : IEdocCatetoryUnitOfWork
{
    private readonly IEdocCatetoryService _edocCatetory;

    public EdocCatetoryUnitOfWork(IEdocCatetoryService edocCatetory)
    {
        _edocCatetory = edocCatetory;
    }

    public async Task<ActionResponse<IEnumerable<GuidItemModel>>> ComboAsync(string email) => await _edocCatetory.ComboAsync(email);

    public async Task<ActionResponse<IEnumerable<EdocCategory>>> GetAsync(PaginationDTO pagination, string Email) => await _edocCatetory.GetAsync(pagination, Email);

    public async Task<ActionResponse<EdocCategory>> GetAsync(Guid id) => await _edocCatetory.GetAsync(id);

    public async Task<ActionResponse<EdocCategory>> UpdateAsync(EdocCategory modelo) => await _edocCatetory.UpdateAsync(modelo);

    public async Task<ActionResponse<EdocCategory>> AddAsync(EdocCategory modelo, string Email) => await _edocCatetory.AddAsync(modelo, Email);

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _edocCatetory.DeleteAsync(id);
}