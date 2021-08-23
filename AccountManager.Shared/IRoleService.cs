using System.Threading.Tasks;
using AccountCommands.Commands;
using BaseCommands;

namespace AccountManager.Shared
{
    public interface IRoleService
    {
        Task<BaseCommandResponse> ActionDefineAdd(ActionDefineAddCommand command);
    }
}