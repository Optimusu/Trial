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

    private readonly IStringLocalizer _localizer;

    // Inyección del localizador para soporte multilingüe
    public HttpErrorHandler(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    public Task<ActionResponse<T>> HandleErrorAsync<T>(Exception exception)
    {
        // Mensaje por defecto si no se detecta el tipo de excepción
        string errorMessage = _localizer["Generic_UnexpectedError"].Value;

        if (exception is null)
        {
            errorMessage = _localizer["Generic_NullException"].Value;
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
            errorMessage = _localizer["Generic_Http_BadRequest"].Value + $": {httpEx.Message}";
        }
        // Manejo de errores de Base de Datos
        else if (exception is DbUpdateException dbEx)
        {
            var innerMsg = dbEx.InnerException?.Message?.ToLower() ?? "";

            if (innerMsg.Contains("duplicate key") || innerMsg.Contains("unique constraint"))
            {
                errorMessage = _localizer["Db_Duplicate"].Value;
            }
            else if (innerMsg.Contains("foreign key") || innerMsg.Contains("reference"))
            {
                errorMessage = _localizer["Db_Reference"].Value;
            }
            else
            {
                errorMessage = _localizer["Db_Error"].Value + $": {dbEx.Message}";
            }
        }
        else if (exception is DbUpdateConcurrencyException)
        {
            errorMessage = _localizer["Db_Concurrency"].Value;
        }
        // Manejo genérico para otras excepciones
        else
        {
            errorMessage = _localizer["Generic_Exception"].Value + $": {exception.Message}";
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