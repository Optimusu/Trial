using Trial.Services.ImplementEntties;
using Trial.Services.ImplementSecure;
using Trial.Services.InterfaceEntities;
using Trial.Services.InterfacesSecure;
using Trial.UnitOfWork.ImplementEntities;
using Trial.UnitOfWork.ImplementSecure;
using Trial.UnitOfWork.InterfaceEntities;
using Trial.UnitOfWork.InterfacesSecure;

namespace Trial.AppBack.DependencyInjection
{
    public class UnitOfWorkRegistration
    {
        public static void AddUnitOfWorkRegistration(IServiceCollection services)
        {
            //EntitiesSecurities Software
            services.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();

            //Entities
            services.AddScoped<ICountryUnitOfWork, CountryUnitOfWork>();
            services.AddScoped<ICountryServices, CountryService>();
            services.AddScoped<IStateUnitOfWork, StateUnitOfWork>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityUnitOfWork, CityUnitOfWork>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ISoftPlanUnitOfWork, SoftPlanUnitOfWork>();
            services.AddScoped<ISoftPlanService, SoftPlanService>();
            services.AddScoped<ICorporationUnitOfWork, CorporationUnitOfWork>();
            services.AddScoped<ICorporationService, CorporationService>();
        }
    }
}