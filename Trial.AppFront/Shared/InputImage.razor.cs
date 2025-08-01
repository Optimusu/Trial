using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Trial.AppFront.Shared;

public partial class InputImage
{
    private string? ImageBase64;
    private string? FileName;

    [Parameter] public string? Label { get; set; }
    [Parameter] public string? ImageUrl { get; set; }
    [Parameter] public EventCallback<string> ImageSelected { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (string.IsNullOrWhiteSpace(Label))
        {
            Label = "Imagen";
        }
    }

    private async Task OnChange(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file is null || !(file.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
            || file.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
            || file.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase)))
            return;

        FileName = file.Name;

        var arrBytes = new byte[file.Size];
        await file.OpenReadStream().ReadAsync(arrBytes);
        ImageBase64 = Convert.ToBase64String(arrBytes);
        ImageUrl = null;
        await ImageSelected.InvokeAsync(ImageBase64);
        StateHasChanged();
    }

    private string GetImageSource() =>
    !string.IsNullOrWhiteSpace(ImageBase64)
        ? $"data:image/jpeg;base64,{ImageBase64}"
        : ImageUrl!;
}