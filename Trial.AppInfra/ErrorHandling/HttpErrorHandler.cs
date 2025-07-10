using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Trial.Domain.Resources;
using Trial.DomainLogic.TrialResponse;

namespace Trial.AppInfra.ErrorHandling;

public class HttpErrorHandler
{
    // Si en el futuro querés registrar errores en logs:
    // private readonly ILogger<HttpErrorHandler> _logger;

    // public HttpErrorHandler(ILogger<HttpErrorHandler> logger)
    // {
    //     _logger = logger;
    // }

    private readonly IStringLocalizer<Resource> _localizer;

    // Inyección del localizador para soporte multilingüe
    public HttpErrorHandler(IStringLocalizer<Resource> localizer)
    {
        _localizer = localizer;
    }

    public Task<ActionResponse<T>> HandleErrorAsync<T>(Exception exception)
    {
        // Mensaje por defecto si no se detecta el tipo de excepción
        string errorMessage = _localizer["Generic_UnexpectedError"];

        if (exception is null)
        {
            errorMessage = _localizer["Generic_NullException"];
            return Task.FromResult(new ActionResponse<T>
            {
                WasSuccess = false,
                Message = errorMessage,
                Result = default
            });
        }

        // Manejo de errores HTTP
        if (exception is HttpRequestException httpEx)
        {
            errorMessage = _localizer["Http_BadRequest"] + $": {httpEx.Message}";
        }
        // Manejo de errores de Base de Datos
        else if (exception is DbUpdateException dbEx)
        {
            var innerMsg = dbEx.InnerException?.Message?.ToLower() ?? "";

            if (innerMsg.Contains("duplicate key") || innerMsg.Contains("unique constraint"))
            {
                errorMessage = _localizer["Db_Duplicate"];
            }
            else if (innerMsg.Contains("foreign key") || innerMsg.Contains("reference"))
            {
                errorMessage = _localizer["Db_Reference"];
            }
            else
            {
                errorMessage = _localizer["Db_Error"] + $": {dbEx.Message}";
            }
        }
        else if (exception is DbUpdateConcurrencyException)
        {
            errorMessage = _localizer["Db_Concurrency"];
        }
        // Manejo genérico para otras excepciones
        else
        {
            errorMessage = _localizer["Generic_Exception"] + $": {exception.Message}";
        }

        // _logger?.LogError(exception, "Ocurrió un error: {Message}", errorMessage);

        return Task.FromResult(new ActionResponse<T>
        {
            WasSuccess = false,
            Message = errorMessage,
            Result = default
        });
    }
}