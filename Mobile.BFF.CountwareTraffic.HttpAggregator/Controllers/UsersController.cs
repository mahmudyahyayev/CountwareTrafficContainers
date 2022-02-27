using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Mobile.BFF.CountwareTraffic.HttpAggregator.Controllers
{
    [Authorize]
    public class UsersController : BaseController<UsersController>
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserService _userService;
        public UsersController(IServiceProvider provider, ILogger<UsersController> logger, IHttpContextAccessor contextAccessor, UserService userService) : base(provider, logger, contextAccessor.HttpContext)
        {
            _logger = logger;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<OkObjectResult> GetToken([FromForm] TokenRequest request)  //username: admin  password: A1w2e3r4t5*
        {
            var response = await _userService.GetTokenAsync(request);

            return Ok(response);
        }
    }
}
