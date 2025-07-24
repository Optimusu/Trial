using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.DomainLogic.ResponsesSec;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Auth;

public partial class RecoverPassword
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigation { get; set; } = null!;
    [Inject] private HttpResponseHandler _httpHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;

    private EmailDTO EmailDTO = new();

    private async Task SendRecoverPasswordEmailTokenAsync()
    {
        var responseHttp = await _repository.PostAsync("/api/v1/accounts/RecoverPassword", EmailDTO);
        if (await _httpHandler.HandleErrorAsync(responseHttp)) return;

        _modalService.Close();
        _navigation.NavigateTo("/");
        await _sweetAlert.FireAsync("Email Sent", "We've sent you instructions to reset your password. Please check your inbox.", SweetAlertIcon.Success);
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