using System;
using System.Threading.Tasks;
using BaseApplication.Interfaces;
using BaseCommands;
using Configs;
using EnumDefine;
using EventBus;
using Extensions;
using Microsoft.Extensions.Logging;

namespace BaseApplication.Implements
{
    public class BaseService: BaseEventHandler, IBaseService
    {
        //protected readonly IContextService ContextService;
        private new readonly ILogger<BaseService> _logger;

        protected BaseService(/*IContextService contextService, */ILogger<BaseService> logger) : base(logger)
        {
           //ContextService = contextService;
            _logger = logger;
        }

        protected async Task<BaseCommandResponse> ProcessCommand(Func<BaseCommandResponse, Task> processFunc)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            try
            {
                await processFunc(response);
            }
            catch (Exception e)
            {
                if (e.Data.Contains(Constant.ErrorCodeEnum) &&
                    Enum.TryParse(e.Data[Constant.ErrorCodeEnum].AsString(), out ErrorCodeEnum _))
                {
                    response.SetFail((ErrorCodeEnum) e.Data[Constant.ErrorCodeEnum]);
                }
                else
                {
                    response.SetFail(e.Message);
                }

                //ContextService.LogError(e, e.Message);
            }
            return response;
        }

        protected async Task<BaseCommandResponse<T>> ProcessCommand<T>(Func<BaseCommandResponse<T>, Task> processFunc)
        {
            BaseCommandResponse<T> response = new BaseCommandResponse<T>();
            try
            {
                await processFunc(response);
            }
            catch (Exception e)
            {
                if (e.Data.Contains(Constant.ErrorCodeEnum))
                {
                    response.SetFail(e.Data[Constant.ErrorCodeEnum].AsString());
                }
                else
                {
                    response.SetFail(e.Message);
                }
            }
            return response;
        }

        protected void LogError(Exception exception, string message)
        {
            _logger.LogError(exception, message);
        }

        protected void LogError(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }

        protected void LogInformation(Exception exception, string message)
        {
            _logger.LogInformation(exception, message);
        }

        protected void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        protected void LogWarning(Exception exception, string message)
        {
            _logger.LogWarning(exception, message);
        }

        protected void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}