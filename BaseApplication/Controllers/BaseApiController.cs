using System;
using System.Threading.Tasks;
using BaseApplication.Interfaces;
using BaseReadModels;
using Configs;
using EnumDefine;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BaseApplication.Controllers
{
    /*[Produces("application/json")]
    [Route("api/[controller]/[action]")]*/
    public class BaseApiController : ControllerBase
    {
        //protected readonly IContextService ContextService;
        private readonly ILogger<BaseApiController> _logger;

        public BaseApiController(IHttpContextAccessor httpContextAccessor)
        {
            var httpContextAccessor1 = httpContextAccessor;
            if (httpContextAccessor1.HttpContext != null)
            {
                //ContextService = httpContextAccessor1.HttpContext.RequestServices.GetRequiredService<IContextService>();
                _logger = httpContextAccessor1.HttpContext.RequestServices
                    .GetRequiredService<ILogger<BaseApiController>>();
            }
        }

        protected async Task<BaseResponse> ProcessRequest(Func<BaseResponse, Task> processFunc)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                await processFunc(response);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("HTTP status code: 401"))
                {
                    response.SetFail(ErrorCodeEnum.Unauthorized);
                }
                else
                {
                    if (e.Data.Contains(Constant.ErrorCodeEnum) && Enum.TryParse(
                        e.Data[Constant.ErrorCodeEnum].AsString(), out EnumDefine.ErrorCodeEnum errorCodeValue))
                    {
                        response.SetFail((EnumDefine.ErrorCodeEnum) e.Data[Constant.ErrorCodeEnum]);
                    }
                    else
                    {
                        response.SetFail(e.Message);
                    }
                }
                //ContextService.LogError(e, e.Message);
            }

            return response;
        }

        protected async Task<BaseResponse<T>> ProcessRequest<T>(Func<BaseResponse<T>, Task> processFunc)
        {
            BaseResponse<T> response = new BaseResponse<T>();
            try
            {
                await processFunc(response);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("HTTP status code: 401"))
                {
                    response.SetFail(ErrorCodeEnum.Unauthorized);
                }
                else
                {
                    if (e.Data.Contains(Constant.ErrorCodeEnum) && Enum.TryParse(
                        e.Data[Constant.ErrorCodeEnum].AsString(), out EnumDefine.ErrorCodeEnum errorCodeValue))
                    {
                        response.SetFail((EnumDefine.ErrorCodeEnum) e.Data[Constant.ErrorCodeEnum]);
                    }
                    else
                    {
                        response.SetFail(e.Message);
                    }
                }
                //ContextService.LogError(e, e.Message);
            }

            return response;
        }
    }
}