using Microsoft.AspNetCore.Mvc;
using Trial.DomainLogic.TrialResponse;

namespace Trial.AppInfra.ErrorHandling;

public static class ResponseHelper
{
    public static IActionResult Format<T>(ActionResponse<T> response)
    {
        if (!response.WasSuccess)
            return new BadRequestObjectResult(response.Message);

        if (response.Result is IEnumerable<object> enumerable && !enumerable.Any())
            return new NoContentResult();

        if (response.Result == null)
            return new NoContentResult();

        return new OkObjectResult(response.Result);
    }
}