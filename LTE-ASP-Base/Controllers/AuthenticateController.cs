using SystemManager.Shared;
using AccountManager.Shared;
using BaseApplication.Controllers;
using LTE_ASP_Base.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTE_ASP_Base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController: BaseApiController
    {
        private readonly IUserService _userService;

        private readonly ICommonService _commonService;
            
        public AuthenticateController(IHttpContextAccessor httpContextAccessor, IUserService userService, ICommonService commonService) : base(httpContextAccessor)
        {
            _userService = userService;
            _commonService = commonService;
        }
        
        /*[HttpPost("authenticate")]
        public IActionResult Login(AuthenticateRequest request)
        {
            var response = _userService.Authenticate(request);
            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});
            return Ok(response);
        }*/
    }
}