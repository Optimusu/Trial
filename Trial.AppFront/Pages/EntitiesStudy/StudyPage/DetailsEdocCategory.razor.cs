using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.AppFront.Pages.EntitiesStudy.EdocStudyPage;
using Trial.Domain.EntitiesStudy;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.EntitiesStudy.StudyPage;

public partial class DetailsEdocCategory
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    [Parameter] public Guid Id { get; set; } //StudyId:Guid

    private Dictionary<Guid, bool> ExpandedCategories = new();
    private Dictionary<Guid, List<EdocStudy>> EdocStudiesByCategory = new();

    private string Filter { get; set; } = string.Empty;

    private int CurrentPage = 1;  //Pagina seleccionada
    private int TotalPages;      //Cantidad total de paginas
    private int PageSize = 15;  //Cantidad de registros por pagina

    private const string baseUrl = "api/v1/edoccategories";
    public Study? Study { get; set; }
    public List<EdocCategory>? EdocCategories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Cargar();
    }

    private async Task ToggleEdocStudiesAsync(Guid categoryId)
    {
        if (ExpandedCategories.ContainsKey(categoryId))
            ExpandedCategories[categoryId] = !ExpandedCategories[categoryId];
        else
            ExpandedCategories[categoryId] = true;

        if (ExpandedCategories[categoryId] && !EdocStudiesByCategory.ContainsKey(categoryId))
        {
            var url = $"api/v1/edocstudies?guidid={categoryId}";
            var response = await _repository.GetAsync<List<EdocStudy>>(url);
            bool errorHandled = await _responseHandler.HandleErrorAsync(response);
            if (!errorHandled)
            {
                EdocStudiesByCategory[categoryId] = response.Response ?? new();
            }
        }
    }

    private async Task SelectedPage(int page)
    {
        CurrentPage = page;
        await Cargar(page);
    }

    private async Task Cargar(int page = 1)
    {
        var url = $"{baseUrl}?guidid={Id}&page={page}&recordsnumber={PageSize}";
        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }
        var responseHttp = await _repository.GetAsync<List<EdocCategory>>(url);
        // Centralizamos el manejo de errores
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled)
        {
            _navigationManager.NavigateTo("/studies");
            return;
        }

        EdocCategories = responseHttp.Response;
        TotalPages = int.Parse(responseHttp.HttpResponseMessage.Headers.GetValues("Totalpages").FirstOrDefault()!);

        await LoadStudy();
    }

    private async Task LoadStudy()
    {
        var responseHTTP = await _repository.GetAsync<Study>($"/api/v1/studies/{Id}");
        bool errorHandler = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandler)
        {
            _navigationManager.NavigateTo("/");
            _navigationManager.NavigateTo($"/studies");
            return;
        }
        Study = responseHTTP.Response;
    }

    private async Task ShowModalEdocAsync(Guid? id = null)
    {
        var parameters = new Dictionary<string, object>
            {
                { "Id", id! },
                { "Idstudy", Id! },
                { "Title", "Create Document"  }
            };
        await _modalService.ShowAsync<CreateEdocStudy>(parameters);
    }

    private async Task ShowModalAsync(Guid? id = null, bool isEdit = false)
    {
        if (isEdit)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Id", id! },
                { "Title", "Edit Edoc Category"  }
            };
            await _modalService.ShowAsync<EditEdocCatetory>(parameters);
        }
        else
        {
            var parameters = new Dictionary<string, object>
            {
                { "Id", Id! },
                { "Title", "Create Edoc Category"  }
            };
            await _modalService.ShowAsync<CreateEdocCategory>(parameters);
        }
    }

    private async Task DeleteAsync(Guid id)
    {
        var result = await _sweetAlert.FireAsync(new SweetAlertOptions
        {
            Title = Messages.DeleteConfirmationTitle,
            Text = Messages.DeleteConfirmationText,
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
            ConfirmButtonText = Messages.DeleteConfirmButton,
            CancelButtonText = Messages.DeleteCancelButton
        });

        if (result.IsDismissed || result.Value != "true")
            return;

        var responseHttp = await _repository.DeleteAsync($"{baseUrl}/{id}");
        var errorHandler = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandler)
            return;

        await _sweetAlert.FireAsync(Messages.DeleteSuccessTitle, Messages.DeleteSuccessMessage, SweetAlertIcon.Success);
        await Cargar();
    }
}