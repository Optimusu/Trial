using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Trial.AppFront.Shared
{
    public partial class AuthLinks
    {
        [Inject] private NavigationManager _navigation { get; set; } = null!;

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        private string? photoUser;
        private string? LogoCorp;
        private string? NameCorp;

        protected override async Task OnParametersSetAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var claims = authenticationState.User.Claims.ToList();

            photoUser = claims.FirstOrDefault(x => x.Type == "Photo")?.Value;
            LogoCorp = claims.FirstOrDefault(x => x.Type == "LogoCorp")?.Value;
            NameCorp = claims.FirstOrDefault(x => x.Type == "CorpName")?.Value;
        }

        private void ShowModalLogIn()
        {
            _navigation.NavigateTo("/login");
        }

        private void ShowModalLogOut()
        {
            _navigation.NavigateTo("/logout");
        }

        private void ShowModalRecoverPassword()
        {
            _navigation.NavigateTo("/recover-password");
        }

        private void ShowModalCambiarClave()
        {
            _navigation.NavigateTo("/change-password");
        }
    }
}