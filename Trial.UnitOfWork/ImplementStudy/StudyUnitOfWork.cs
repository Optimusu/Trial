using Trial.Domain.EntitiesStudy;
using Trial.Domain.Enum;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesStudy;
using Trial.UnitOfWork.InterfacesStudy;

namespace Trial.UnitOfWork.ImplementStudy;

public class StudyUnitOfWork : IStudyUnitOfWork
{
    private readonly IStudyService _studyService;

    public StudyUnitOfWork(IStudyService studyService)
    {
        _studyService = studyService;
    }

    public async Task<ActionResponse<IEnumerable<EnumItemModel>>> ComboAsync() => await _studyService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Study>>> GetAsync(PaginationDTO pagination, string Email) => await _studyService.GetAsync(pagination, Email);

    public async Task<ActionResponse<Study>> GetAsync(Guid id) => await _studyService.GetAsync(id);

    public async Task<ActionResponse<Study>> UpdateAsync(Study modelo) => await _studyService.UpdateAsync(modelo);

    public async Task<ActionResponse<Study>> AddAsync(Study modelo, string Email) => await _studyService.AddAsync(modelo, Email);

    public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _studyService.DeleteAsync(id);
}