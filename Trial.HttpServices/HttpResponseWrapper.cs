using Microsoft.Extensions.Localization;
using System.Net;
using Trial.Domain.Resources;

namespace Trial.HttpServices;

public class HttpResponseWrapper<T>
{
    private readonly IStringLocalizer<Resource> _localizer;

    public HttpResponseWrapper(
        T? response,
        bool error,
        HttpResponseMessage httpResponseMessage,
        IStringLocalizer<Resource> localizer,
        string? errorMessage = null)
    {
        Response = response;
        Error = error;
        HttpResponseMessage = httpResponseMessage;
        ErrorMessage = errorMessage;
        _localizer = localizer;
    }

    public bool Error { get; set; }
    public T? Response { get; set; }
    public HttpResponseMessage HttpResponseMessage { get; set; }
    public string? ErrorMessage { get; set; }

    public async Task<string?> GetErrorMessageAsync()
    {
        if (!Error)
        {
            return null;
        }

        var statusCode = HttpResponseMessage.StatusCode;

        return statusCode switch
        {
            HttpStatusCode.NotFound => _localizer["Error.NotFound"],
            HttpStatusCode.BadRequest => await HttpResponseMessage.Content.ReadAsStringAsync(), // Puedes envolver esto también
            HttpStatusCode.Unauthorized => _localizer["Error.Unauthorized"],
            HttpStatusCode.Forbidden => _localizer["Error.Forbidden"],
            HttpStatusCode.InternalServerError => _localizer["Error.ServerError"],
            HttpStatusCode.RequestTimeout => _localizer["Error.Timeout"],
            HttpStatusCode.ServiceUnavailable => _localizer["Error.ServiceUnavailable"],
            _ => _localizer["Error.Default"]
        };
    }
}