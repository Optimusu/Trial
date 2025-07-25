using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.EntitesSoftSec;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Entities.SoftSecPage;

public partial class EditUsuario
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    [Parameter] public int Id { get; set; }
    [Parameter] public string? Title { get; set; }

    private Usuario? Usuario;
    private string BaseUrl = "/api/v1/usuarios";
    private string BaseView = "/usuarios";

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await _repository.GetAsync<Usuario>($"{BaseUrl}/{Id}");
        if (await _responseHandler.HandleErrorAsync(responseHttp)) return;
        Usuario = responseHttp.Response;
    }

    private async Task Edit()
    {
        var responseHttp = await _repository.PutAsync($"{BaseUrl}", Usuario);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.UpdateSuccessTitle, Messages.UpdateSuccessMessage, SweetAlertIcon.Success);
        _modalService.Close();
        _navigationManager.NavigateTo(BaseView);
    }

    private void Return()
    {
        _modalService.Close();
        _navigationManager.NavigateTo($"{BaseView}");
    }
}