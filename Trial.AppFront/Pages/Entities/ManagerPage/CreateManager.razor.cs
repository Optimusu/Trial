using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.Entities;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Entities.ManagerPage;

public partial class CreateManager
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public string? Title { get; set; }

    private Manager _Manager = new() { Active = true };

    private string BaseUrl = "/api/v1/managers";
    private string BaseView = "/managers";

    private async Task Create()
    {
        if (_Manager.CorporationId == 0)
        {
            await _sweetAlert.FireAsync(Messages.ValidationWarningTitle, Messages.ValidationWarningMessage, SweetAlertIcon.Warning);
            return;
        }
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", _Manager);
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