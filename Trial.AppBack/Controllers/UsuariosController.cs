using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Trial.AppBack.Helper;
using Trial.AppInfra.ErrorHandling;
using Trial.Domain.EntitesSoftSec;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.ResponsesSec;
using Trial.UnitOfWork.InterfacesSecure;

namespace Trial.AppBack.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/usuarios")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer _localizer;

        public UsuariosController(IUsuarioUnitOfWork usuarioUnitOfWork,
            IConfiguration configuration, IStringLocalizer localizer)
        {
            _unitOfWork = usuarioUnitOfWork;
            _configuration = configuration;
            _localizer = localizer;
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
        public async Task<IActionResult> GetAsync(int id)
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
        public async Task<IActionResult> PutAsync(Usuario modelo)
        {
            try
            {
                var response = await _unitOfWork.UpdateAsync(modelo, _configuration["UrlFrontend"]!);
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
        public async Task<IActionResult> PostAsync(Usuario modelo)
        {
            try
            {
                ClaimsDTOs userClaimsInfo = User.GetEmailOrThrow(_localizer);
                var response = await _unitOfWork.AddAsync(modelo, _configuration["UrlFrontend"]!, userClaimsInfo.Email);
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
        public async Task<IActionResult> DeleteAsync(int id)
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