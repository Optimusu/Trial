using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Trial.AppBack.Controllers.Common;
using Trial.AppBack.Helper;
using Trial.AppInfra.ErrorHandling;
using Trial.Domain.Entities;
using Trial.Domain.Resources;
using Trial.UnitOfWork.InterfaceEntities;

namespace Trial.AppBack.Controllers;

[ApiVersion("1.0")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
[Route("api/v{version:apiVersion}/countries")]
public class CountriesController : GenericController<Country, ICountryUnitOfWork>
{
    private readonly ICountryUnitOfWork _countryUnitOfWork;
    private readonly IStringLocalizer<Resource> _localizer;

    public CountriesController(ICountryUnitOfWork unitOfWork, IStringLocalizer<Resource> localizer) : base(unitOfWork)
    {
        _countryUnitOfWork = unitOfWork;
        _localizer = localizer;
    }

    [HttpGet("loadCombo")]
    public async Task<IActionResult> GetComboAsync()
    {
        try
        {
            string email = User.GetEmailOrThrow(_localizer);
            var response = await _countryUnitOfWork.ComboAsync(email);
            return ResponseHelper.Format(response);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message); // Multilenguaje ya incluido
        }
    }
}