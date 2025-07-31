﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Trial.AppInfra.ErrorHandling;
using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.UnitOfWork.InterfacesGen;

namespace Trial.AppBack.Controllers.EntitiesGen
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cros")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Administrator")]
    [ApiController]
    public class CrosController : ControllerBase
    {
        private readonly ICroUnitOfWork _unitOfWork;
        private readonly IStringLocalizer _localizer;

        public CrosController(ICroUnitOfWork unitOfWork, IStringLocalizer localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        [HttpGet("loadCombo")]
        public async Task<IActionResult> GetComboAsync()
        {
            try
            {
                var response = await _unitOfWork.ComboAsync();
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
                var response = await _unitOfWork.GetAsync(pagination);
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
        public async Task<IActionResult> PutAsync(Cro modelo)
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
        public async Task<IActionResult> PostAsync(Cro modelo)
        {
            try
            {
                var response = await _unitOfWork.AddAsync(modelo);
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