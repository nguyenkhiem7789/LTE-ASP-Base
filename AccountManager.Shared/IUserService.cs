using System.Collections.Generic;
using System.Threading.Tasks;
using AccountCommands.Commands;
using AccountCommands.Queries;
using AccountReadModels;
using BaseCommands;

namespace AccountManager.Shared
{
    public interface IUserService
    {
        Task<BaseCommandResponse<RUser[]>> Gets(UserGetsQuery query);
        Task<BaseCommandResponse<RUser[]>> GetAll();
        Task<BaseCommandResponse<RUser>> GetById(UserGetByIdQuery query);
        Task<BaseCommandResponse<string>> Add(UserAddCommand command);
        Task<BaseCommandResponse> Change(UserChangeCommand command);
        Task<BaseCommandResponse<RLoginModel>> Authenticate(AuthenticateQuery query);
    }
}