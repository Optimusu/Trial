using Mapster;
using MapsterMapper;
using Trial.AppInfra.EmailHelper;
using Trial.AppInfra.ErrorHandling;
using Trial.AppInfra.FileHelper;
using Trial.AppInfra.Mappings;
using Trial.AppInfra.Transactions;
using Trial.AppInfra.UserHelper;
using Trial.AppInfra.UtilityTools;

namespace Trial.AppBack.DependencyInjection
{
    public class InfraRegistration
    {
        public static void AddInfraRegistration(IServiceCollection services, IConfiguration config)
        {
            // Manejo de Errores
            services.AddScoped<HttpErrorHandler>();

            // Manejo de transacciones por request
            services.AddScoped<ITransactionManager, TransactionManager>();

            // Utilidades para manejo de Imagenes o Archivos
            services.AddScoped<IFileStorage, FileStorage>();

            // Utilidades para autenticación y gestión de usuarios
            services.AddScoped<IUserHelper, UserHelper>();

            // Herramientas generales sin estado
            services.AddTransient<IUtilityTools, UtilityTools>();

            // Servicio de envío de correos
            services.AddTransient<IEmailHelper, EmailHelper>();

            // Configuración y mapeo con Mapster
            MapsterConfig.RegisterMappings();
            services.AddSingleton(TypeAdapterConfig.GlobalSettings);
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddScoped<IMapperService, MapperService>();
        }
    }
}