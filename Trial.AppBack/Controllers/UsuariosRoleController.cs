﻿using Asp.Versioning;
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

namespace Trial.AppBack.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/usuarioRoles")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
[ApiController]
public class UsuariosRoleController : ControllerBase
{
    private readonly IUsuarioRoleUnitOfWork _usuarioRoleUnitOfWork;
    private readonly IStringLocalizer _localizer;

    public UsuariosRoleController(IUsuarioRoleUnitOfWork usuarioRoleUnitOfWork, IStringLocalizer localizer)
    {
        _usuarioRoleUnitOfWork = usuarioRoleUnitOfWork;
        _localizer = localizer;
    }

    [HttpGet("loadCombo")]
    public async Task<IActionResult> GetComboAsync()
    {
        try
        {
            var response = await _usuarioRoleUnitOfWork.ComboAsync();
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
            var response = await _usuarioRoleUnitOfWork.GetAsync(pagination);
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
            var response = await _usuarioRoleUnitOfWork.GetAsync(id);
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
    public async Task<IActionResult> PostAsync(UsuarioRole modelo)
    {
        try
        {
            ClaimsDTOs userClaimsInfo = User.GetEmailOrThrow(_localizer);
            var response = await _usuarioRoleUnitOfWork.AddAsync(modelo, userClaimsInfo.Email);
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
            var response = await _usuarioRoleUnitOfWork.DeleteAsync(id);
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