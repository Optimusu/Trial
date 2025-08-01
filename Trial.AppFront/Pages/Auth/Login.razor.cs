using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Trial.AppFront.AuthenticationProviders;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.DomainLogic.ResponsesSec;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Auth;

public partial class Login
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigation { get; set; } = null!;
    [Inject] private ILoginService _loginService { get; set; } = null!;
    [Inject] private HttpResponseHandler _httpHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    private LoginDTO loginDTO = new();
    private bool rememberMe;

    private async Task LoginAsync()
    {
        var responseHttp = await _repository.PostAsync<LoginDTO, TokenDTO>("/api/v1/accounts/Login", loginDTO);
        if (await _httpHandler.HandleErrorAsync(responseHttp)) return;
        await _loginService.LoginAsync(responseHttp.Response!.Token);
        _navigation.NavigateTo("/");
        _modalService.Close();
    }

    private async Task OpenRecoverPasswordModal()
    {
        await _modalService.ShowAsync<RecoverPassword>();
    }

    private void CloseModal()
    {
        _modalService.Close();
    }

    private string GetDisplayName<T>(Expression<Func<T>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            var property = memberExpression.Member as PropertyInfo;
            if (property != null)
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name!;
                }
            }
        }
        return "Unspecified text";
    }
}