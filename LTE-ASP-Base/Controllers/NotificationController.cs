using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SystemCommands.Commands;
using SystemManager.Shared;
using BaseApplication.Controllers;
using BaseReadModels;
using LTE_ASP_Base.Mappings;
using LTE_ASP_Base.Models;
using LTE_ASP_Base.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationCommands.Commands;
using NotificationCommands.Queries;
using NotificationManager.Services;
using NotificationManager.Shared;
using NotificationReadModels;

namespace LTE_ASP_Base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController: BaseApiController
    {
        private readonly ISignalRService _signalRService;

        private readonly INotificationService _notificationService;

        private readonly ICommonService _commonService;
        
        public NotificationController(
            IHttpContextAccessor httpContextAccessor, 
            ISignalRService signalRService, 
            INotificationService notificationService,
            ICommonService commonService
            ) : base(httpContextAccessor)
        {
            _signalRService = signalRService;
            _notificationService = notificationService;
            _commonService = commonService;
        }

        [HttpPost("Add")]
        public async Task<BaseResponse<object>> Add([FromBody] NotificationAddRequest request)
        {
            return await ProcessRequest<object>(async (response) =>
            {
                var results = NotificationAddValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var code = await _commonService.GetNextCode(new GetNextCodeQuery()
                {
                    ProcessUid = string.Empty,
                    TypeName = typeof(RNotification).FullName
                });
                var result = await _notificationService.Add(new NotificationAddCommand()
                {
                    Code = code,
                    ObjectId = code,
                    Title = request.Title,
                    Content = request.Content
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
        public async ValueTask<BaseResponse> Change([FromBody] NotificationChangeRequest request)
        {
            return await ProcessRequest(async response =>
            {
                var results = NotificationChangeValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }

                var result = await _notificationService.Change(new NotificationChangeCommand()
                {
                    Id = request.Id,
                    Title = request.Title,
                    Content = request.Content
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
        public async Task<BaseResponse<object>> Gets([FromBody] NotificationGetsRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                var result = await _notificationService.Gets(new NotificationGetsQuery()
                {
                    keyword = request.Keyword
                });
                if (!result.Status || result.Data == null)
                {
                    response.SetFail(result.Messages);
                    return;
                }

                response.Data = new
                {
                    TotalRow = result.TotalRow,
                    Models = result.Data?.Select(NotificationMapping.ToModel)
                };
                response.SetSuccess();
            });
        }
        
        [HttpPost("GetById")]
        public async Task<BaseResponse<object>> GetById([FromBody] NotificationGetByIdRequest request)
        {
            return await ProcessRequest<object>(async response =>
            {
                var results = NotificationGetByIdValidator.ValidateModel(request);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var result = await _notificationService.GetById(new NotificationGetByIdQuery()
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
                    Models = NotificationMapping.ToModel(result.Data)
                };
                response.SetSuccess();
            });
        }

        [HttpPost("Test")]
        public async Task<string> Test([FromBody] SignalRRequest request)
        {
            var retMessage = string.Empty;
            try
            {
                await _signalRService.SendMessage("this is tessst");
                retMessage = "success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }
}