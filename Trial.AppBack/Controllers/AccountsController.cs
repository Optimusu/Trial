using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Trial.AppInfra.ErrorHandling;
using Trial.DomainLogic.ResponsesSec;
using Trial.UnitOfWork.InterfacesSecure;

namespace Trial.AppBack.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer _localizer;

        public AccountsController(IAccountUnitOfWork accountUnitOfWork,
            IConfiguration configuration, IStringLocalizer localizer)
        {
            _unitOfWork = accountUnitOfWork;
            _configuration = configuration;
            _localizer = localizer;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO modelo)
        {
            try
            {
                var response = await _unitOfWork.LoginAsync(modelo);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
        }

        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPasswordAsync([FromBody] EmailDTO modelo)
        {
            try
            {
                var response = await _unitOfWork.RecoverPasswordAsync(modelo, _configuration["UrlFrontend"]!);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }

            //var response = await _accountUnitOfWork.RecoverPasswordAsync(modelo, _configuration["UrlFrontend"]!);
            //if (!response.WasSuccess)
            //{
            //    return BadRequest(response.Message);
            //}
            //return Ok(response.Result);
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDTO modelo)
        {
            try
            {
                var response = await _unitOfWork.ResetPasswordAsync(modelo);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }
            //var response = await _accountUnitOfWork.ResetPasswordAsync(modelo);
            //if (!response.WasSuccess)
            //{
            //    return BadRequest(response.Message);
            //}
            //return Ok(response.Result);
        }

        [HttpPost("changePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDTO modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(_localizer["Generic_InvalidModel"]);
            }
            if (string.IsNullOrWhiteSpace(User.Identity!.Name!))
            {
                return Unauthorized(_localizer["Generic_AuthRequired"]);
            }
            string UserName = User.Identity!.Name!;

            try
            {
                var response = await _unitOfWork.ChangePasswordAsync(modelo, UserName);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(_localizer["Generic_InvalidModel"]);
            //}
            //if (string.IsNullOrWhiteSpace(User.Identity!.Name!))
            //{
            //    return Unauthorized(_localizer["Generic_AuthRequired"]);
            //}
            //string UserName = User.Identity!.Name!;

            //var response = await _unitOfWork.ChangePasswordAsync(modelo, UserName);
            //if (!response.WasSuccess)
            //{
            //    return BadRequest(response.Message);
            //}
            //return Ok(response.Result);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
        {
            try
            {
                token = token.Replace(" ", "+");
                var response = await _unitOfWork.ConfirmEmailAsync(userId, token);
                return ResponseHelper.Format(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, _localizer["Generic_UnexpectedError"] + ": " + ex.Message);
            }

            //token = token.Replace(" ", "+");
            //var response = await _unitOfWork.ConfirmEmailAsync(userId, token);
            //if (!response.WasSuccess)
            //{
            //    return BadRequest(response.Message);
            //}
            //return Ok(response.Result);
        }
    }
}