using Microsoft.Extensions.Localization;
using System.Security.Claims;
using Trial.Domain.Resources;

namespace Trial.AppBack.Helper;

public static class ClaimsPrincipalExtensions
{
    public static string GetEmailOrThrow(this ClaimsPrincipal user, IStringLocalizer<Resource> localizer)
    {
        string? email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (string.IsNullOrWhiteSpace(email))
            throw new ApplicationException(localizer["Generic_AuthEmailFail"]);

        return email;
    }
}