using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using Trial.AppFront;
using Trial.AppFront.AuthenticationProviders;
using Trial.AppFront.Helpers;
using Trial.Domain.Resources;
using Trial.HttpServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7229") });

//Sistema de Seguridad
builder.Services.AddAuthorizationCore();
//Manejar el SweetAlert de mensajes
builder.Services.AddSweetAlert2();

// Registrar HttpResponseHandler
builder.Services.AddScoped<HttpResponseHandler>();

// Reemplazar la configuracion del IRepository
builder.Services.AddScoped(sp =>
{
    var jsRuntime = sp.GetRequiredService<IJSRuntime>();
    var httpClient = sp.GetRequiredService<HttpClient>();
    var localizer = sp.GetRequiredService<IStringLocalizer<Resource>>();

    return new Repository(
        httpClient,
        async () =>
        {
            var token = await jsRuntime.GetLocalStorage("TOKEN_KEY");
            return Convert.ToString(token);
        },
        localizer
    );
});

// Registrar IRepository como Repository
builder.Services.AddScoped<IRepository>(sp => sp.GetRequiredService<Repository>());

//Authentication Provider
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

//Localizacion
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
//Detectar cultura automaticamente
var host = builder.Build();
await CultureInitializer.SetCultureFromBrowserAsync(host.Services.GetRequiredService<IJSRuntime>());

await host.RunAsync();