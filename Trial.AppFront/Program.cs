using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Trial.AppFront;
using Trial.AppFront.AuthenticationProviders;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.HttpServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7229") });

// Sistema de Seguridad
builder.Services.AddAuthorizationCore();

// Manejar el SweetAlert de mensajes
builder.Services.AddSweetAlert2();

// Registrar HttpResponseHandler
builder.Services.AddScoped<HttpResponseHandler>();

// Reemplazar la configuración del IRepository
builder.Services.AddScoped(sp =>
{
    var jsRuntime = sp.GetRequiredService<IJSRuntime>();
    var httpClient = sp.GetRequiredService<HttpClient>();

    return new Repository(
        httpClient,
        async () =>
        {
            var token = await jsRuntime.GetLocalStorage("TOKEN_KEY");
            return string.IsNullOrWhiteSpace(token) ? null : token;
        }
    );
});

// Registrar IRepository como Repository
builder.Services.AddScoped<IRepository>(sp => sp.GetRequiredService<Repository>());
//Para manejar los Modales como MudBlazor
builder.Services.AddScoped<ModalService>();

//Authentication Provider
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

await builder.Build().RunAsync();