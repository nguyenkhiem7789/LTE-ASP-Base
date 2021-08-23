using System;
using System.Threading.Tasks;
using SystemCommands.Commands;
using SystemManager.Shared;
using AccountCommands.Commands;
using AccountManager.Shared;
using AccountReadModels;
using BaseApplication.Controllers;
using BaseReadModels;
using LTE_ASP_Base.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTE_ASP_Base.Controllers
{
    [ApiController]
    
    [Route("[controller]")]
    public class UsersController: BaseApiController
    {
        private readonly IUserService _userService;

        private readonly ICommonService _commonService;
        
        public UsersController(IHttpContextAccessor httpContextAccessor, IUserService userService, ICommonService commonService) : base(httpContextAccessor)
        {
            _userService = userService;
            _commonService = commonService;
        }

        [HttpPost("add")]
        public async Task<BaseResponse<object>> Add([FromBody] UserAddRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                var code = await _commonService.GetNextCode(new GetNextCodeQuery()
                {
                    ProcessUid = string.Empty,
                    TypeName = typeof(RUser).FullName
                });
                var result = await _userService.Add(new UserAddCommand()
                {
                    Code = code,
                    ObjectId = code,
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