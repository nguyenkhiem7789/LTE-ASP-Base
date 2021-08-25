﻿using System;
using System.Linq;
using System.Threading.Tasks;
using SystemCommands.Commands;
using SystemManager.Shared;
using AccountCommands.Commands;
using AccountCommands.Queries;
using AccountManager.Shared;
using AccountReadModels;
using BaseApplication.Controllers;
using BaseReadModels;
using LTE_ASP_Base.Models;
using LTE_ASP_Base.Validations;
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

        [HttpPost("Add")]
        public async Task<BaseResponse<object>> Add([FromBody] UserAddRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                var results = UserAddValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
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
                    Email = request.Email,
                    Status = request.Status,
                    Password = request.Password
                });
                if (!result.Status)
                {
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = new
                {
                    Models = result.Data
                };
                response.SetSuccess();
            });
        }

        [HttpPost("Change")]
        public async ValueTask<BaseResponse> Change([FromBody] UserChangeRequest request)
        {
            return await ProcessRequest(async (response) =>
            {
                var results = UserChangeValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var result = await _userService.Change(new UserChangeCommand()
                {
                    Id = request.Id,
                    FullName = request.FullName,
                    Email = request.Email,
                    Status = request.Status
                });
                if (!result.Status)
                {
                    response.SetFail(result.Messages);
                    return;
                }
                response.SetSuccess();
            });
        }

        [HttpPost("Gets")]
        public async Task<BaseResponse<object>> Gets([FromBody] UserGetRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                var result = await _userService.Gets(new UserGetsQuery()
                {
                    Keyword = request.Keyword
                });
                if (!result.Status)
                {
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = new
                {
                    Models = result.Data?.Select(x => new UserModel()
                    {
                        Id = x.Id,
                        FullName = x.FullName,
                        Email = x.Email,
                        Status = x.Status
                    })
                };
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