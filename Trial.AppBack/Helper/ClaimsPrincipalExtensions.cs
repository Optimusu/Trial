using Microsoft.Extensions.Localization;
using System.Security.Claims;
using Trial.DomainLogic.ResponsesSec;

namespace Trial.AppBack.Helper;

public static class ClaimsPrincipalExtensions
{
    public static UserClaimsInfo GetEmailOrThrow(this ClaimsPrincipal user, IStringLocalizer localizer)
    {
        if (user?.Identity?.IsAuthenticated != true)
            throw new ApplicationException(localizer["Generic_AuthRequired"].Value);

        string? email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        string? id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        string? corporateId = user.Claims.FirstOrDefault(c => c.Type == "CorporateId")?.Value;
        string? role = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

        if (string.IsNullOrWhiteSpace(email))
            throw new ApplicationException(localizer["Generic_AuthEmailFail"].Value);

        if (string.IsNullOrWhiteSpace(id))
            throw new ApplicationException(localizer["Generic_AuthIdFail"].Value);

        if (string.IsNullOrWhiteSpace(role))
            throw new ApplicationException(localizer["Generic_AuthRoleFail"].Value);

        return new UserClaimsInfo
        {
            Email = email,
            Id = id,
            CorporateId = corporateId,
            Role = role
        };
    }
}