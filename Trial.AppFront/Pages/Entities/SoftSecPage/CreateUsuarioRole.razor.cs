using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.EntitesSoftSec;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Entities.SoftSecPage;

public partial class CreateUsuarioRole
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public int Id { get; set; }
    [Parameter] public string? Title { get; set; }

    private UsuarioRole UsuarioRole = new();

    private string BaseUrl = "/api/v1/usuarioRoles";
    private string BaseView = "/usuarios/details";

    private async Task Create()
    {
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", UsuarioRole);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.CreateSuccessTitle, Messages.CreateSuccessMessage, SweetAlertIcon.Success);
        _modalService.Close();
        _navigationManager.NavigateTo(BaseView);
    }

    private void Return()
    {
        _modalService.Close();
        _navigationManager.NavigateTo($"{BaseView}");
    }
}