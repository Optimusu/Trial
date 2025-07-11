using Trial.AppInfra.ErrorHandling;
using Trial.Services.ImplementEntties;
using Trial.Services.InterfaceEntities;
using Trial.UnitOfWork.ImplementEntities;
using Trial.UnitOfWork.InterfaceEntities;

namespace Trial.AppBack.DependencyInjection
{
    public class UnitOfWorkRegistration
    {
        public static void AddUnitOfWorkRegistration(IServiceCollection services)
        {
            //Entities
            services.AddScoped<ICountryUnitOfWork, CountryUnitOfWork>();
            services.AddScoped<ICountryServices, CountryService>();
        }
    }
}