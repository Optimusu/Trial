using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Auth;

public partial class ConfirmEmail
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigation { get; set; } = null!;
    [Inject] private HttpResponseHandler _httpHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;

    [Parameter, SupplyParameterFromQuery] public string UserId { get; set; } = string.Empty;
    [Parameter, SupplyParameterFromQuery] public string Token { get; set; } = string.Empty;

    private async Task ConfirmAccountAsync()
    {
        var responseHttp = await _repository.GetAsync($"/api/v1/accounts/ConfirmEmail/?userId={UserId}&token={Token}");
        if (await _httpHandler.HandleErrorAsync(responseHttp))
        {
            _navigation.NavigateTo("/");
            return;
        }
        await _sweetAlert.FireAsync("Email Confirmed", "Your account has been successfully activated.", SweetAlertIcon.Success);
        _navigation.NavigateTo("/");
        await _modalService.ShowAsync<Login>();
    }
}