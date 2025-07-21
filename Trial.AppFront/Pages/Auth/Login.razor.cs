using Microsoft.AspNetCore.Components;
using Trial.AppFront.AuthenticationProviders;
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

    private LoginDTO loginDTO = new();
    private bool rememberMe;

    private async Task LoginAsync()
    {
        var responseHttp = await _repository.PostAsync<LoginDTO, TokenDTO>("/api/accounts/Login", loginDTO);
        if (await _httpHandler.HandleErrorAsync(responseHttp)) return;
        await _loginService.LoginAsync(responseHttp.Response!.Token);
        _navigation.NavigateTo("/");
    }
}