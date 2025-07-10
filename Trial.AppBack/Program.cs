using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using Trial.AppBack.Data;
using Trial.AppBack.DependencyInjection;
using Trial.AppBack.LoadCountries;
using Trial.AppInfra;
using Trial.DomainLogic.ResponsesSec;
using AppUser = Trial.Domain.Entities.User;

var builder = WebApplication.CreateBuilder(args);

// 🌐 Localización y soporte multilingüe
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// 🧠 Habilita acceso directo a recursos desde clases (servicios, helpers, etc.)
builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
builder.Services.AddSingleton(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));

var supportedCultures = new[] { "es", "en" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var cultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

// 🔄 Configuración de serialización
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// 📄 Swagger + Versionado
builder.Services.AddOpenApi();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

//Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders Backend - V1", Version = "1.0" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "Orders Backend - V2", Version = "2.0" });

    options.DocInclusionPredicate((version, desc) =>
    {
        var versions = desc.ActionDescriptor.EndpointMetadata.OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
        return versions.Any(v => $"v{v.MajorVersion}" == version);
    });

    options.CustomSchemaIds(type => type.Name.Replace("Controller", "").Replace("V", ""));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.<br />
                        Enter 'Bearer' [space] and then your token in the text input below.<br />
                        Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    options.OperationFilter<RemoveVersionParameterFilter>();
});

// 🛠️ Base de datos
builder.Services.AddDbContext<DataContext>(x =>
    x.UseSqlServer("name=DefaultConnection", option => option.MigrationsAssembly("Trial.AppBack")));

//Para realizar logueo de los usuarios
builder.Services.AddIdentity<AppUser, IdentityRole>(cfg =>
{
    //Agregamos Validar Correo para dar de alta al Usuario
    cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
    cfg.SignIn.RequireConfirmedEmail = true;

    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false;
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
    //Sistema para bloquear por 5 minutos al usuario por intento fallido
    cfg.Lockout.MaxFailedAccessAttempts = 3;
    cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  //TODO: Cambiar Tiempo de Bloqueo a Usuarios
    cfg.Lockout.AllowedForNewUsers = true;
}).AddDefaultTokenProviders()  //Complemento Validar Correo
  .AddEntityFrameworkStores<DataContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie()
    .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
        ClockSkew = TimeSpan.Zero
    });

// 🔐 Configuración AppSettings
builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection("SendGrid"));
builder.Services.Configure<ImgSetting>(builder.Configuration.GetSection("ImgSoftware"));
builder.Services.Configure<JwtKeySetting>(options =>
{
    options.jwtKey = builder.Configuration.GetValue<string>("jwtKey");
});

// 🧪 Seed inicial
builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IApiService, ApiService>();

// 🔧 Accesos auxiliares
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

// 🧱 Registro de infraestructura
InfraRegistration.AddInfraRegistration(builder.Services, builder.Configuration);
// 🔄 Registro de UnitOfWork y servicios
UnitOfWorkRegistration.AddUnitOfWorkRegistration(builder.Services);

// 🌐 CORS
string? frontUrl = builder.Configuration["UrlFrontend"];
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins(frontUrl!)
           .AllowAnyHeader()
           .AllowAnyMethod()
           .WithExposedHeaders(new[] { "Totalpages", "Counting" });
    });
});

var app = builder.Build();

// 🌍 Middleware para aplicar localización por idioma
var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

// 🌱 Ejecutar seeding de la base de datos al arrancar
SeedData(app);

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using var scope = scopedFactory!.CreateScope();
    var seeder = scope.ServiceProvider.GetService<SeedDb>();
    seeder?.SeedAsync().Wait();
}

// 🚀 Entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders Backend - V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Orders Backend - V2");
    });

    Task.Run(() => OpenBrowser("https://localhost:7229/swagger"));
}

// 📦 Archivos estáticos
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "Images")),
    RequestPath = "/Images"
});

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// 🖥️ Apertura automática del navegador con Swagger
static void OpenBrowser(string url)
{
    try
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al abrir el navegador: {ex.Message}");
    }
}

// 🧽 Filtro Swagger para eliminar parámetro de versión
public class RemoveVersionParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var versionParam = operation.Parameters?.FirstOrDefault(p => p.Name == "version");
        if (versionParam != null)
        {
            operation.Parameters!.Remove(versionParam);
        }
    }
}