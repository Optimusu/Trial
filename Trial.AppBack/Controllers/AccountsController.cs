using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Trial.AppBack.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;
        private readonly IConfiguration _configuration;

        public AccountsController(IAccountUnitOfWork accountUnitOfWork, IConfiguration configuration)
        {
            _accountUnitOfWork = accountUnitOfWork;
            _configuration = configuration;
        }
    }
}