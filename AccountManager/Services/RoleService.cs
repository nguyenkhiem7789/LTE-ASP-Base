using System.Threading.Tasks;
using AccountCommands.Commands;
using AccountManager.Shared;
using BaseApplication.Implements;
using BaseCommands;
using Microsoft.Extensions.Logging;

namespace AccountManager
{
    public class RoleService: BaseService, IRoleService
    {
        protected RoleService(ILogger<BaseService> logger) : base(logger)
        {
        }

        public Task<BaseCommandResponse> ActionDefineAdd(ActionDefineAddCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}