using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Trial.AppFront.Helpers;
using Trial.Domain.EntitiesGen;
using Trial.Domain.EntitiesStudy;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.EntitiesStudy.StudyPage;

public partial class FormStudy
{
    //Services

    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    //Parameters

    [Parameter, EditorRequired] public Study Study { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
    [Parameter, EditorRequired] public bool IsEditControl { get; set; }

    //Local State

    private List<TherapeuticArea>? TherapeuticAreas;
    private List<Enrolling>? Enrollings;
    private List<Indication>? Indications;
    private List<Sponsor>? Sponsors;
    private List<Irb>? Irbs;
    private List<Cro>? Cros;
    private string BaseView = "/studies";
    private string BaseComboTherapeutic = "/api/v1/therapeutics/loadCombo";
    private string BaseComboEnrolling = "/api/v1/enrollings/loadCombo";
    private string BaseComboIndication = "/api/v1/indications/loadCombo";
    private string BaseComboSponsor = "/api/v1/sponsors/loadCombo";
    private string BaseComboIrb = "/api/v1/irbs/loadCombo";
    private string BaseComboCro = "/api/v1/cros/loadCombo";

    protected override async Task OnInitializedAsync()
    {
        await LoadTherapeutic();
        await LoadEnrolling();
        await LoadIndication();
        await LoadSponsor();
        await LoadIrb();
        await LoadCro();
    }

    private async Task LoadCro()
    {
        var responseHttp = await _repository.GetAsync<List<Cro>>($"{BaseComboCro}");
        bool errorHandler = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
        {
            _navigationManager.NavigateTo($"{BaseView}");
            return;
        }
        Cros = responseHttp.Response;
    }

    private void CroChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Study.IrbId = selectedId;
        }
    }

    private async Task LoadIrb()
    {
        var responseHttp = await _repository.GetAsync<List<Irb>>($"{BaseComboIrb}");
        bool errorHandler = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
        {
            _navigationManager.NavigateTo($"{BaseView}");
            return;
        }
        Irbs = responseHttp.Response;
    }

    private void IrbChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Study.IrbId = selectedId;
        }
    }

    private async Task LoadSponsor()
    {
        var responseHttp = await _repository.GetAsync<List<Sponsor>>($"{BaseComboSponsor}");
        bool errorHandler = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
        {
            _navigationManager.NavigateTo($"{BaseView}");
            return;
        }
        Sponsors = responseHttp.Response;
    }

    private void SponsorChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Study.SponsorId = selectedId;
        }
    }

    private async Task LoadIndication()
    {
        var responseHttp = await _repository.GetAsync<List<Indication>>($"{BaseComboIndication}");
        bool errorHandler = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
        {
            _navigationManager.NavigateTo($"{BaseView}");
            return;
        }
        Indications = responseHttp.Response;
    }

    private void IndicationChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Study.IndicationId = selectedId;
        }
    }

    private async Task LoadTherapeutic()
    {
        var responseHttp = await _repository.GetAsync<List<TherapeuticArea>>($"{BaseComboTherapeutic}");
        bool errorHandler = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
        {
            _navigationManager.NavigateTo($"{BaseView}");
            return;
        }
        TherapeuticAreas = responseHttp.Response;
    }

    private void TherapeuticChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Study.TherapeuticAreaId = selectedId;
        }
    }

    private async Task LoadEnrolling()
    {
        var responseHttp = await _repository.GetAsync<List<Enrolling>>($"{BaseComboEnrolling}");
        bool errorHandler = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
        {
            _navigationManager.NavigateTo($"{BaseView}");
            return;
        }
        Enrollings = responseHttp.Response;
    }

    private void EnrollingChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int selectedId))
        {
            Study.EnrollingId = selectedId;
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