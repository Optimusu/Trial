using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.Entities;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Entities.Coporations;

public partial class CreateCorporation
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public string? Title { get; set; }

    private Corporation _Corporation = new() { Active = true };

    private string BaseUrl = "/api/v1/corporations";
    private string BaseView = "/corporations";

    private async Task Create()
    {
        if (_Corporation.SoftPlanId == 0 || _Corporation.CountryId == 0)
        {
            await _sweetAlert.FireAsync(Messages.ValidationWarningTitle, Messages.ValidationWarningMessage, SweetAlertIcon.Warning);
            return;
        }
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", _Corporation);
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