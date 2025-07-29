using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.GenericoModal;
using Trial.AppFront.Helpers;
using Trial.Domain.EntitiesGen;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.EntitiesGen.TherapeuticAreaPage;

public partial class IndexTherapeuticArea
{
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private ModalService _modalService { get; set; } = null!;
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    private string Filter { get; set; } = string.Empty;

    private int CurrentPage = 1;  //Pagina seleccionada
    private int TotalPages;      //Cantidad total de paginas
    private int PageSize = 2;  //Cantidad de registros por pagina

    private const string baseUrl = "api/v1/therapeutics";
    public List<TherapeuticArea>? TherapeuticAreas { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Cargar();
    }

    private async Task SelectedPage(int page)
    {
        CurrentPage = page;
        await Cargar(page);
    }

    private async Task SetFilterValue(string value)
    {
        Filter = value;
        await Cargar();
    }

    private async Task Cargar(int page = 1)
    {
        var url = $"{baseUrl}?page={page}&recordsnumber={PageSize}";
        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }
        var responseHttp = await _repository.GetAsync<List<TherapeuticArea>>(url);
        // Centralizamos el manejo de errores
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHttp);
        if (errorHandled)
        {
            _navigationManager.NavigateTo("/");
            return;
        }

        TherapeuticAreas = responseHttp.Response;
        TotalPages = int.Parse(responseHttp.HttpResponseMessage.Headers.GetValues("Totalpages").FirstOrDefault()!);
    }

    private async Task ShowModalAsync(int id = 0, bool isEdit = false)
    {
        if (isEdit)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Id", id },
                { "Title", "Edit Soft Plan"  }
            };
            await _modalService.ShowAsync<EditTherapeuticArea>(parameters);
        }
        else
        {
            var parameters = new Dictionary<string, object>
            {
                { "Title", "Create Soft Plan"  }
            };
            await _modalService.ShowAsync<CreateTherapeuticArea>(parameters);
        }
    }

    private async Task DeleteAsync(int id)
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