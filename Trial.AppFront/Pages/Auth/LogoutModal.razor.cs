using Microsoft.AspNetCore.Components;
using Trial.AppFront.AuthenticationProviders;
using Trial.AppFront.GenericoModal;

namespace Trial.AppFront.Pages.Auth;

public partial class LogoutModal
{
    [Inject] private ILoginService LoginService { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    private async Task ConfirmLogout()
    {
        await LoginService.LogoutAsync();
        Navigation.NavigateTo("/");
        _modalService.Close();
    }

    private void CloseModal()
    {
        _modalService.Close();
    }
}