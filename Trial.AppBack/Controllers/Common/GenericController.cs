using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Trial.AppInfra.ErrorHandling;
using Trial.DomainLogic.Pagination;
using Trial.UnitOfWork.BaseInterface;

namespace Trial.AppBack.Controllers.Common;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class GenericController<T, TUnitOfWork> : ControllerBase
    where T : class
    where TUnitOfWork : IBaseUnitOfWork<T>
{
    protected readonly TUnitOfWork _unitOfWork;

    protected GenericController(TUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAll([FromQuery] PaginationDTO pagination)
    {
        var response = await _unitOfWork.GetAsync(pagination);
        return ResponseHelper.Format(response);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetAsync(int id)
    {
        var response = await _unitOfWork.GetAsync(id);
        return ResponseHelper.Format(response);
    }

    [HttpPost]
    public virtual async Task<IActionResult> PostAsync([FromBody] T model)
    {
        var response = await _unitOfWork.AddAsync(model);
        return ResponseHelper.Format(response);
    }

    [HttpPut]
    public virtual async Task<IActionResult> PutAsync([FromBody] T model)
    {
        var response = await _unitOfWork.UpdateAsync(model);
        return ResponseHelper.Format(response);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await _unitOfWork.DeleteAsync(id);
        return ResponseHelper.Format(response);
    }
}