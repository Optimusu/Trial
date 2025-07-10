using Microsoft.AspNetCore.Mvc;

namespace Trial.AppBack.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
    }
}