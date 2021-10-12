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
using LTE_ASP_Base.Shared.Requests;
using LTE_ASP_Base.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTE_ASP_Base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController: BaseApiController
    {
        private readonly IRoleService _roleService;

        private readonly ICommonService _commonService;
        
        public RoleController(IHttpContextAccessor httpContextAccessor, IRoleService roleService, ICommonService commonService) : base(httpContextAccessor)
        {
            _roleService = roleService;
            _commonService = commonService;
        }

        [HttpPost("Add")]
        public async Task<BaseResponse<object>> Add([FromBody] RoleAddRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                var results = RoleAddValidator.ValidateModel(request);
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
                var result = await _roleService.Add(new RoleChangeCommand()
                {
                    Code = code,
                    ObjectId = code,
                    Name = request.Name,
                    Status = request.Status
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
        public async ValueTask<BaseResponse<object>> Change([FromBody] RoleChangeRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                var results = RoleChangeValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var result = await _roleService.Change(new RoleChangeCommand()
                {
                    Name = request.Name,
                    Status = request.Status
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

        [HttpPost("GetById")]
        public async ValueTask<BaseResponse<object>> GetById([FromBody] RoleGetByIdRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                var results = RoleGetByIdValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }

                var result = await _roleService.GetById(new RoleGetByIdQuery()
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
                    Models = result.Data
                };
                response.SetSuccess();
            });
        }

        [HttpPost("Gets")]
        public async ValueTask<BaseResponse<object>> Gets([FromBody] RoleGetsRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                var result = await _roleService.Gets(new RoleGetsQuery()
                {
                    Keyword = request.Keyword,
                    Status = request.Status
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
    }
}