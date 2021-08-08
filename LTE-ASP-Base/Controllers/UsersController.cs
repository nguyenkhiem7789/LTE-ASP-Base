using System;
using System.Threading.Tasks;
using AccountCommands.Commands;
using AccountManager.Shared;
using BaseApplication.Controllers;
using BaseReadModels;
using LTE_ASP_Base.Helpers;
using LTE_ASP_Base.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTE_ASP_Base.Controllers
{
    [ApiController]
    
    [Route("[controller]")]
    public class UsersController: BaseApiController
    {
        private readonly IAccountService _accountService;
        
        public UsersController(IHttpContextAccessor httpContextAccessor, IAccountService accountService) : base(httpContextAccessor)
        {
            _accountService = accountService;
        }

        [HttpGet("test")]
        public string test()
        {
            return "This is test!!!!";
        }
        
        [HttpPost("addUser")]
        public async Task<BaseResponse<object>> AddUser([FromBody] UserAddRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                var result = await _accountService.Add(new AccountAddCommand()
                {
                    FullName = request.FullName,
                    Password = request.Password
                });
                if (!result.Status)
                {
                    response.SetFail(result.Messages);
                    return;
                }

                response.SetSuccess();
            });
        }

        /*[HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userService.Authenticate(request);
            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});
            return Ok(response);
        }

        [MyAuthorize]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }*/
    }
}