using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.Entities;
using Trial.Domain.EntitiesStudy;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.EntitiesStudy.StudyPage;

public partial class EditEdocCatetory
{
    //Services

    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public Guid Id { get; set; } //EdocCatetoryId
    [Parameter] public string? Title { get; set; }

    //Local State

    private EdocCategory? _edocCategory;
    private const string BaseUrl = "/api/v1/edoccategories";
    private const string BaseView = "/studies/edoccategory";

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await _repository.GetAsync<EdocCategory>($"{BaseUrl}/{Id}");
        if (await _responseHandler.HandleErrorAsync(responseHttp)) return;

        _edocCategory = responseHttp.Response;
    }

    private async Task Edit()
    {
        var responseHttp = await _repository.PutAsync($"{BaseUrl}", _edocCategory);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.UpdateSuccessTitle, Messages.UpdateSuccessMessage, SweetAlertIcon.Success);
        _modalService.Close();
        _navigationManager.NavigateTo("/");
        _navigationManager.NavigateTo($"{BaseView}/{_edocCategory!.StudyId}");
    }

    private void Return()
    {
        _modalService.Close();
        _navigationManager.NavigateTo($"{BaseView}/{_edocCategory!.StudyId}");
    }
}