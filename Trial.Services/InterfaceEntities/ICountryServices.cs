using Trial.Domain.Entities;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.BaseInterface;

namespace Trial.Services.InterfaceEntities;

public interface ICountryServices : IBaseService<Country>
{
    Task<ActionResponse<IEnumerable<Country>>> ComboAsync(string email);
}