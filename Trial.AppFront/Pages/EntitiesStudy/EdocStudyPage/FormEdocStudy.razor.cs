using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Trial.Domain.EntitiesStudy;

namespace Trial.AppFront.Pages.EntitiesStudy.EdocStudyPage;

public partial class FormEdocStudy
{
    [Parameter, EditorRequired] public EdocStudy EdocStudy { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback OnSubmit { get; set; }
    [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }

    private async Task HandleFile(IBrowserFile file)
    {
        // Puedes guardar el archivo, convertirlo a base64, etc.
        var buffer = new byte[file.Size];
        await file.OpenReadStream().ReadAsync(buffer);
        var base64 = Convert.ToBase64String(buffer);
        EdocStudy.FileNameOriginal = file.Name;
        EdocStudy.ImgBase64 = base64;
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