using System;
using System.Diagnostics;
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
using LTE_ASP_Base.Mappings;
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
                if (!result.Status || result.Data == null)
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
                    Keyword = request.Keyword,
                    Status = request.Status,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                });
                if (!result.Status || result.Data == null)
                {
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = new
                {
                    TotalRow = result.TotalRow,
                    Models = result.Data?.Select(UserMapping.ToModel)
                };
                response.SetSuccess();
            });
        }

        [HttpGet("GetAll")]
        public async Task<BaseResponse<object>> GetAll()
        {
            return await ProcessRequest<object>(async response =>
            {
                var result = await _userService.GetAll();
                if (!result.Status || result.Data == null)
                {
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = new
                {
                    TotalRow = result.TotalRow,
                    Models = result.Data?.Select(UserMapping.ToModel)
                };
                response.SetSuccess();
            });
        }

        [HttpPost("GetById")]
        public async Task<BaseResponse<object>> GetById([FromBody] UserGetByIdRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                var results = UserGetByIdValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var result = await _userService.GetById(new UserGetByIdQuery()
                {
                    Id = request.Id
                });
                if (!result.Status || result.Data == null) 
                {
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = new
                {
                    Models = UserMapping.ToModel(result.Data)
                };
                response.SetSuccess();
            });
        }
        
        [HttpPost("Login")]
        public async Task<BaseResponse<object>> Login([FromBody] AuthenticateRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                var results = LoginValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var result = await _userService.Authenticate(new AuthenticateQuery()
                {
                    Username = request.Username,
                    Password = request.Password
                });
                if (!result.Status || result.Data == null)
                {
                    response.SetFail(result.Messages);
                    return;
                }
                response.Data = new
                {
                    Models = LoginMapping.ToModel(result.Data)
                };
                response.SetSuccess();
            });
        }
        
    }
}