using Trial.Services.ImplementEntties;
using Trial.Services.ImplementGen;
using Trial.Services.ImplementSecure;
using Trial.Services.ImplementStudy;
using Trial.Services.InterfaceEntities;
using Trial.Services.InterfacesGen;
using Trial.Services.InterfacesSecure;
using Trial.Services.InterfacesStudy;
using Trial.UnitOfWork.ImplementEntities;
using Trial.UnitOfWork.ImplementGen;
using Trial.UnitOfWork.ImplementSecure;
using Trial.UnitOfWork.ImplementStudy;
using Trial.UnitOfWork.InterfaceEntities;
using Trial.UnitOfWork.InterfacesGen;
using Trial.UnitOfWork.InterfacesSecure;
using Trial.UnitOfWork.InterfacesStudy;

namespace Trial.AppBack.DependencyInjection
{
    public class UnitOfWorkRegistration
    {
        public static void AddUnitOfWorkRegistration(IServiceCollection services)
        {
            //EntitiesSecurities Software
            services.AddScoped<IAccountUnitOfWork, AccountUnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUsuarioUnitOfWork, UsuarioUnitOfWork>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRoleUnitOfWork, UsuarioRoleUnitOfWork>();
            services.AddScoped<IUsuarioRoleService, UsuarioRoleService>();

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
            services.AddScoped<IManagerUnitOfWork, ManagerUnitOfWork>();
            services.AddScoped<IManagerService, ManagerService>();

            //EntitiesGen
            services.AddScoped<IDocumentTypeUnitOfWork, DocumentTypeUnitOfWork>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<ITherapeuticAreaUnitOfWork, TherapeuticAreaUnitOfWork>();
            services.AddScoped<ITherapeuticAreaService, TherapeuticAreaService>();
            services.AddScoped<IIndicationUnitOfWork, IndicationUnitOfWork>();
            services.AddScoped<IIndicationService, IndicationService>();
            services.AddScoped<ISponsorUnitOfWork, SponsorUnitOfWork>();
            services.AddScoped<ISponsorService, SponsorService>();
            services.AddScoped<IEnrollingUnitOfWork, EnrollingUnitOfWork>();
            services.AddScoped<IEnrollingService, EnrollingService>();
            services.AddScoped<IIrbUnitOfWork, IrbUnitOfWork>();
            services.AddScoped<IIrbService, IrbService>();
            services.AddScoped<ICroUnitOfWork, CroUnitOfWork>();
            services.AddScoped<ICroService, CroService>();

            //EntitiesStudies
            services.AddScoped<IStudyUnitOfWork, StudyUnitOfWork>();
            services.AddScoped<IStudyService, StudyService>();
            services.AddScoped<IEdocCatetoryUnitOfWork, EdocCatetoryUnitOfWork>();
            services.AddScoped<IEdocCatetoryService, EdocCatetoryService>();
            services.AddScoped<IEdocStudyUnitOfWork, EdocStudyUnitOfWork>();
            services.AddScoped<IEdocStudyService, EdocStudyService>();
        }
    }
}