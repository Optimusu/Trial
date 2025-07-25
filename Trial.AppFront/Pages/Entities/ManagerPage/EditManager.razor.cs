using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.Entities;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Entities.ManagerPage;

public partial class EditManager
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    [Parameter] public int Id { get; set; }
    [Parameter] public string? Title { get; set; }

    private Manager? _Manager;
    private string BaseUrl = "/api/v1/managers";
    private string BaseView = "/managers";

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await _repository.GetAsync<Manager>($"{BaseUrl}/{Id}");
        if (await _responseHandler.HandleErrorAsync(responseHttp)) return;
        _Manager = responseHttp.Response;
    }

    private async Task Edit()
    {
        if (_Manager!.CorporationId == 0)
        {
            await _sweetAlert.FireAsync(Messages.ValidationWarningTitle, Messages.ValidationWarningMessage, SweetAlertIcon.Warning);
            return;
        }
        var responseHttp = await _repository.PutAsync($"{BaseUrl}", _Manager);
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