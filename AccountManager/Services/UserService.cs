using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AccountCommands.Commands;
using AccountCommands.Queries;
using AccountDomains;
using AccountManager.Shared;
using AccountManager.Validations;
using AccountReadModels;
using AccountRepository;
using BaseApplication.Implements;
using BaseApplication.Interfaces;
using BaseCommands;
using Microsoft.Extensions.Logging;

namespace AccountManager
{
    public class UserService : BaseService, IUserService
    {

        private IUserRepository _userRepository;
        
        public UserService(/*IContextService contextService, */ILogger<BaseService> logger, IUserRepository userRepository) : base(/*contextService, */logger)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseCommandResponse<RUser[]>> Gets(UserGetsQuery query)
        {
            return await ProcessCommand<RUser[]>(async response =>
            {
                var users = await _userRepository.Gets(query: query);
                response.Data = users;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RUser>> GetById(UserGetByIdQuery query)
        {
            return await ProcessCommand<RUser>(async response =>
            {
                var user = await _userRepository.GetById(query: query);
                response.Data = user;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<string>> Add(UserAddCommand command)
        {
            return await ProcessCommand<string>(async response =>
            {
                var results = UserAddValidator.ValidateModel(command);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var user = new User(command);
                await _userRepository.Add(user);
                response.Data = user.Id;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Change(UserChangeCommand command)
        {
            return await ProcessCommand(async response =>
            {
                var results = UserChangeValidator.ValidateModel(command);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var rUser = await _userRepository.GetById(new UserGetByIdQuery()
                {
                    Id = command.Id
                });
                var user = new User(rUser);
                user.Change(command: command);
                await _userRepository.Change(user: user);
                response.SetSuccess();
            });
        }
    }
}