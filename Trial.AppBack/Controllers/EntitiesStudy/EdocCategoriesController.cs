using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Trial.AppBack.Helper;
using Trial.AppInfra.ErrorHandling;
using Trial.Domain.EntitiesStudy;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.UnitOfWork.InterfacesStudy;

namespace Trial.AppBack.Controllers.EntitiesStudy
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/edoccategories")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [ApiController]
    public class EdocCategoriesController : ControllerBase
    {
        private readonly IEdocCatetoryUnitOfWork _unitOfWork;
        private readonly IStringLocalizer _localizer;

        public EdocCategoriesController(IEdocCatetoryUnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        [HttpGet("loadCombo")]
        public async Task<IActionResult> GetComboAsync(string email)
        {
            try
            {
                var response = await _unitOfWork.ComboAsync(email);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDTO pagination)
        {
            try
            {
                ClaimsDTOs userClaimsInfo = User.GetEmailOrThrow(_localizer);
                var response = await _unitOfWork.GetAsync(pagination, userClaimsInfo.Email);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var response = await _unitOfWork.GetAsync(id);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(EdocCategory modelo)
        {
            try
            {
                var response = await _unitOfWork.UpdateAsync(modelo);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(EdocCategory modelo)
        {
            try
            {
                ClaimsDTOs userClaimsInfo = User.GetEmailOrThrow(_localizer);
                var response = await _unitOfWork.AddAsync(modelo, userClaimsInfo.Email);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var response = await _unitOfWork.DeleteAsync(id);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message); // Ya está localizado
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }
    }
}