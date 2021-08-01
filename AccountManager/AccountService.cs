using System.Threading.Tasks;
using AccountCommands.Commands;
using AccountCommands.Queries;
using AccountManager.Shared;
using AccountReadModels;
using BaseApplication.Implements;
using BaseApplication.Interfaces;
using BaseCommands;
using Microsoft.Extensions.Logging;

namespace AccountManager
{
    public class AccountService : BaseService, IAccountService
    {
        protected AccountService(IContextService contextService, ILogger<BaseService> logger) : base(contextService, logger)
        {
        }

        public Task<BaseCommandResponse<RUser[]>> Gets(AccountGetsQuery query)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseCommandResponse<string>> Add(AccountAddCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}