using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.EntitiesStudy;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.EntitiesStudy.EdocStudyPage;

public partial class CreateEdocStudy
{
    //Services

    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;

    //Parameters

    [Parameter] public Guid Id { get; set; }  //EdocCategoryId
    [Parameter] public Guid Idstudy { get; set; }  //EdocCategoryId
    [Parameter] public string? Title { get; set; }

    //Local State

    private EdocStudy _edocStudy = new();
    private string BaseUrl = "/api/v1/edocstudies";
    private string BaseView = "/studies/edoccategory";

    private async Task Create()
    {
        _edocStudy.EdocCategoryId = Id;
        _edocStudy.StudyId = Idstudy;
        var responseHttp = await _repository.PostAsync($"{BaseUrl}", _edocStudy);
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled) return;

        await _sweetAlert.FireAsync(Messages.CreateSuccessTitle, Messages.CreateSuccessMessage, SweetAlertIcon.Success);
        _modalService.Close();
        _navigationManager.NavigateTo("/");
        _navigationManager.NavigateTo($"{BaseView}/{Idstudy}");
    }

    private void Return()
    {
        _modalService.Close();
        _navigationManager.NavigateTo("/");
        _navigationManager.NavigateTo($"{BaseView}/{Idstudy}");
    }
}