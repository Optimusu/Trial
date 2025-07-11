using Trial.Domain.Entities;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfaceEntities;
using Trial.UnitOfWork.BaseImplement;
using Trial.UnitOfWork.InterfaceEntities;

namespace Trial.UnitOfWork.ImplementEntities;

public class CountryUnitOfWork : BaseUnitOfWork<Country, ICountryServices>, ICountryUnitOfWork
{
    public CountryUnitOfWork(ICountryServices service) : base(service)
    {
    }

    public Task<ActionResponse<IEnumerable<Country>>> ComboAsync(string email) => _service.ComboAsync(email);
}