using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.Entities;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Entities.SoftPlanPage;

public partial class EditSoftPlan
{
    //Services

    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public int Id { get; set; }
    [Parameter] public string? Title { get; set; }

    //Local State

    private SoftPlan? _softplan;
    private const string BaseUrl = "/api/v1/softplans";
    private const string BaseView = "/softplans";

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await _repository.GetAsync<SoftPlan>($"{BaseUrl}/{Id}");
        if (await _responseHandler.HandleErrorAsync(responseHttp)) return;

        _softplan = responseHttp.Response;
    }

    private async Task Edit()
    {
        var responseHttp = await _repository.PutAsync($"{BaseUrl}", _softplan);
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