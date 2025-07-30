using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.UnitOfWork.InterfacesGen;

public interface IDocumentTypeUnitOfWork
{
    Task<ActionResponse<IEnumerable<DocumentType>>> ComboAsync();

    Task<ActionResponse<IEnumerable<DocumentType>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<DocumentType>> GetAsync(int id);

    Task<ActionResponse<DocumentType>> UpdateAsync(DocumentType modelo);

    Task<ActionResponse<DocumentType>> AddAsync(DocumentType modelo, string Email);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}