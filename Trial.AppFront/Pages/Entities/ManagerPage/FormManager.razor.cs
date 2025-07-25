using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Trial.AppFront.Helpers;
using Trial.Domain.Entities;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Entities.ManagerPage;

public partial class FormManager
{
    //Services

    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    //Parameters

    [Parameter, EditorRequired] public Manager Manager { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
    [Parameter, EditorRequired] public bool IsEditControl { get; set; }

    //Local State

    private List<Corporation>? Corporations;
    private DateTime? DateMin = new DateTime(2025, 1, 1);
    private string? ImageUrl;
    private string BaseView = "/managers";
    private string BaseComboCompany = "/api/v1/corporations/loadCombo";

    protected override async Task OnInitializedAsync()
    {
        await LoadCorporation();
        if (IsEditControl)
        {
            ImageUrl = Manager.ImageFullPath;
        }
    }

    private async Task LoadCorporation()
    {
        var responseHttp = await _repository.GetAsync<List<Corporation>>($"{BaseComboCompany}");
        bool errorHandler = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
        {
            _navigationManager.NavigateTo($"{BaseView}");
            return;
        }
        Corporations = responseHttp.Response;
    }

    private void ImageSelected(string imagenBase64)
    {
        Manager.ImgBase64 = imagenBase64;
        ImageUrl = null;
    }

    private void CorporationChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Manager.CorporationId = selectedId;
        }
    }

    private string GetDisplayName<T>(Expression<Func<T>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            var property = memberExpression.Member as PropertyInfo;
            if (property != null)
            {
                var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name!;
                }
            }
        }
        return "Unspecified text";
    }
}