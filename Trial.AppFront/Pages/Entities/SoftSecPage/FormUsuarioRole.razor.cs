using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Trial.AppFront.Helpers;
using Trial.Domain.EntitesSoftSec;
using Trial.Domain.Enum;
using Trial.HttpServices;

namespace Trial.AppFront.Pages.Entities.SoftSecPage;

public partial class FormUsuarioRole
{
    [Inject] private SweetAlertService _sweetAlert { get; set; } = null!;
    [Inject] private IRepository _repository { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; } = null!;
    [Inject] private HttpResponseHandler _responseHandler { get; set; } = null!;

    [Parameter, EditorRequired] public UsuarioRole UsuarioRole { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }

    private EnumItemModel? SelectedUserType;
    private List<EnumItemModel>? ListUserType;

    protected override async Task OnInitializedAsync()
    {
        await LoadRoles();
    }

    private async Task LoadRoles()
    {
        var responseHTTP = await _repository.GetAsync<List<EnumItemModel>>($"api/v1/usuarioRoles/loadCombo");
        bool errorHandled = await _responseHandler.HandleErrorAsync(responseHTTP);
        if (errorHandled) return;
        ListUserType = responseHTTP.Response;
    }

    private void UsertTypeChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e?.Value?.ToString(), out int modelo))
        {
            if (modelo == 2) { UsuarioRole.UserType = UserType.Administrator; }
            if (modelo == 3) { UsuarioRole.UserType = UserType.Coordinator; }
            if (modelo == 4) { UsuarioRole.UserType = UserType.Researcher; }
            if (modelo == 5) { UsuarioRole.UserType = UserType.Monitor; }
        }
    }
}