using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.Net;
using Trial.AppFront.AuthenticationProviders;
using Trial.HttpServices;

namespace Trial.AppFront.Helpers;

public class HttpResponseHandler
{
    private readonly ILoginService _loginService;
    private readonly NavigationManager _navigationManager;
    private readonly SweetAlertService _sweetAlert;

    public HttpResponseHandler(
        ILoginService loginService,
        NavigationManager navigationManager,
        SweetAlertService sweetAlert)
    {
        _loginService = loginService;
        _navigationManager = navigationManager;
        _sweetAlert = sweetAlert;
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
                title = "Access Denied";
                message = "You are not authorized to access this resource.";
                icon = SweetAlertIcon.Error;
                await _loginService.LogoutAsync();
                _navigationManager.NavigateTo("/");
                break;

            case HttpStatusCode.Forbidden:
                title = "Forbidden";
                message = "You don't have permission to access this resource.";
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.NotFound:
                title = "Not Found";
                message = "The requested resource could not be found.";
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.InternalServerError:
                title = "Server Error";
                message = "An unexpected error occurred on the server.";
                icon = SweetAlertIcon.Error;
                break;

            case HttpStatusCode.BadRequest:
                title = errorMessage?.Contains("Invalid credentials", StringComparison.OrdinalIgnoreCase) == true
                    || errorMessage?.Contains("username", StringComparison.OrdinalIgnoreCase) == true
                    || errorMessage?.Contains("password", StringComparison.OrdinalIgnoreCase) == true
                    ? "Login Error"
                    : "Bad Request";
                message = errorMessage ?? "The request is invalid or malformed.";
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.GatewayTimeout:
                title = "Gateway Timeout";
                message = "The server took too long to respond.";
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.ServiceUnavailable:
                title = "Service Unavailable";
                message = "The server is currently unavailable.";
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.BadGateway:
                title = "Bad Gateway";
                message = "The gateway received an invalid response.";
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.RequestTimeout:
                title = "Timeout";
                message = "The request timed out.";
                icon = SweetAlertIcon.Warning;
                break;

            case HttpStatusCode.UnprocessableEntity:
                title = "Invalid Data";
                message = "The server could not process the submitted data.";
                icon = SweetAlertIcon.Warning;
                break;

            default:
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    title = "Error";
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