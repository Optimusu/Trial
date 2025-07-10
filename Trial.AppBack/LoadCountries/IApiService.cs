using Trial.DomainLogic.TrialResponse;

namespace Trial.AppBack.LoadCountries;

public interface IApiService
{
    Task<Response> GetListAsync<T>(string servicePrefix, string controller);
}