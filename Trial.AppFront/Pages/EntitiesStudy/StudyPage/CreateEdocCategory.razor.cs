using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.EntitesSoftSec;
using Trial.Domain.EntitiesStudy;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.EntitiesStudy.StudyPage;

public partial class CreateEdocCategory
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public Guid Id { get; set; }  //StudyId
    [Parameter] public string? Title { get; set; }

    private EdocCategory EdocCategory = new() { Active = true};

    private string BaseUrl = "/api/v1/edoccategories";
    private string BaseView = "/studies/edoccategory";

    private async Task Create()
    {
        EdocCategory.StudyId = Id;
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", EdocCategory);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.CreateSuccessTitle, Messages.CreateSuccessMessage, SweetAlertIcon.Success);
        _modalService.Close();
        _navigationManager.NavigateTo("/");
        _navigationManager.NavigateTo($"{BaseView}/{Id}");
    }

    private void Return()
    {
        _modalService.Close();
        _navigationManager.NavigateTo("/");
        _navigationManager.NavigateTo($"{BaseView}/{Id}");
    }
}