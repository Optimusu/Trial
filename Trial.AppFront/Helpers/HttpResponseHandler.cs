using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Net;
using Trial.AppFront.AuthenticationProviders;
using Trial.Domain.Resources;
using Trial.HttpServices;

namespace Trial.AppFront.Helpers;

public class HttpResponseHandler
{
    private readonly ILoginService _loginService;
    private readonly NavigationManager _navigationManager;
    private readonly SweetAlertService _sweetAlert;
    private readonly IStringLocalizer<Resource> _localizer;

    public HttpResponseHandler(
        ILoginService loginService,
        NavigationManager navigationManager,
        SweetAlertService sweetAlert,
        IStringLocalizer<Resource> localizer)
    {
        _loginService = loginService;
        _navigationManager = navigationManager;
        _sweetAlert = sweetAlert;
        _localizer = localizer;
    }

    public async Task<bool> HandleErrorAsync<T>(HttpResponseWrapper<T> responseHttp)
    {
        if (responseHttp.HttpResponseMessage == null)
            return false;

        var statusCode = responseHttp.HttpResponseMessage.StatusCode;
        var errorMessage = await responseHttp.GetErrorMessageAsync();

        string title, message;
        SweetAlertIcon icon;

        switch (statusCode)
        {
            case HttpStatusCode.Unauthorized:
                title = _localizer["Http.Title.Unauthorized"];
                message = _localizer["Http.Error.Unauthorized"];
                icon = SweetAlertIcon.Error;
                await _loginService.LogoutAsync();
                _navigationManager.NavigateTo("/");
                break;

            case HttpStatusCode.Forbidden:
                title = _localizer["Http.Title.Forbidden"];
                message = _localizer["Http.Error.Forbidden"];
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.NotFound:
                title = _localizer["Http.Title.NotFound"];
                message = _localizer["Http.Error.NotFound"];
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.InternalServerError:
                title = _localizer["Http.Title.ServerError"];
                message = _localizer["Http.Error.ServerError"];
                icon = SweetAlertIcon.Error;
                break;

            case HttpStatusCode.BadRequest:
                title = _localizer["Http.Title.BadRequest"];
                message = errorMessage ?? _localizer["Http.Error.BadRequest"];
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.GatewayTimeout:
                title = _localizer["Http.Title.GatewayTimeout"];
                message = _localizer["Http.Error.GatewayTimeout"];
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.ServiceUnavailable:
                title = _localizer["Http.Title.ServiceUnavailable"];
                message = _localizer["Http.Error.ServiceUnavailable"];
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.BadGateway:
                title = _localizer["Http.Title.BadGateway"];
                message = _localizer["Http.Error.BadGateway"];
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.RequestTimeout:
                title = _localizer["Http.Title.Timeout"];
                message = _localizer["Http.Error.Timeout"];
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.UnprocessableEntity:
                title = _localizer["Http.Title.InvalidData"];
                message = _localizer["Http.Error.InvalidData"];
                icon = SweetAlertIcon.Warning;
                break;

            default:
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    title = _localizer["Http.Title.Default"];
                    message = errorMessage;
                    icon = SweetAlertIcon.Error;
                    break;
                }
                return false;
        }

        await _sweetAlert.FireAsync(title, message, icon);
        return true;
    }
}