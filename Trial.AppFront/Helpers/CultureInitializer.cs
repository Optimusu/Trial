using Microsoft.JSInterop;
using System.Globalization;

namespace Trial.AppFront.Helpers
{
    public class CultureInitializer
    {
        public static async Task SetCultureFromBrowserAsync(IJSRuntime js)
        {
            var language = await js.InvokeAsync<string>("getBrowserLanguage");

            // Normaliza valores como "es-ES" a solo "es"
            var cultureCode = language?.Split('-')[0] ?? "en";

            var culture = new CultureInfo(cultureCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}