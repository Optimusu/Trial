using Trial.Domain.Entities;
using Trial.DomainLogic.TrialResponse;
using Trial.UnitOfWork.BaseInterface;

namespace Trial.UnitOfWork.InterfaceEntities;

public interface ICountryUnitOfWork : IBaseUnitOfWork<Country>
{
    Task<ActionResponse<IEnumerable<Country>>> ComboAsync(string email);
}