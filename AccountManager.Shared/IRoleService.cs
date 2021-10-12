using System.Threading.Tasks;
using AccountCommands.Commands;
using AccountCommands.Queries;
using AccountReadModels;
using BaseCommands;

namespace AccountManager.Shared
{
    public interface IRoleService
    {
        Task<BaseCommandResponse<string>> Add(RoleAddCommand command);
        Task<BaseCommandResponse> ActionDefineAdd(ActionDefineAddCommand command);
        Task<BaseCommandResponse<RRole>> GetById(RoleGetByIdQuery query);
        Task<BaseCommandResponse<string>> Change(RoleChangeCommand command);
        Task<BaseCommandResponse<RRole[]>> Gets(RoleGetsQuery query);
    }
}