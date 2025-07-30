using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesGen;

public interface IDocumentTypeService
{
    Task<ActionResponse<IEnumerable<DocumentType>>> ComboAsync(string Email);

    Task<ActionResponse<IEnumerable<DocumentType>>> GetAsync(PaginationDTO pagination, string Email);

    Task<ActionResponse<DocumentType>> GetAsync(int id);

    Task<ActionResponse<DocumentType>> UpdateAsync(DocumentType modelo);

    Task<ActionResponse<DocumentType>> AddAsync(DocumentType modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}