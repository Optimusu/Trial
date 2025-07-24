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

public partial class ResetPassword
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigation { get; set; } = null!;
    [Inject] private HttpResponseHandler _httpHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;

    private ResetPasswordDTO resetPasswordDTO = new();

    [Parameter, SupplyParameterFromQuery] public string token { get; set; } = string.Empty;

    private async Task ChangePasswordAsync()
    {
        resetPasswordDTO.Token = token;
        var responseHttp = await _repository.PostAsync("/api/v1/accounts/ResetPassword", resetPasswordDTO);
        if (await _httpHandler.HandleErrorAsync(responseHttp)) return;
        await _sweetAlert.FireAsync("Password Updated", "Your password has been changed successfully.", SweetAlertIcon.Success);
        _navigation.NavigateTo("/");
        await _modalService.ShowAsync<Login>();
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