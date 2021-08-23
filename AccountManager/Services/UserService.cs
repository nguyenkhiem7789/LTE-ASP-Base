using System.Diagnostics;
using System.Threading.Tasks;
using AccountCommands.Commands;
using AccountCommands.Queries;
using AccountDomains;
using AccountManager.Shared;
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

        public Task<BaseCommandResponse<RUser[]>> Gets(UserGetsQuery query)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BaseCommandResponse<string>> Add(UserAddCommand command)
        {
            return await ProcessCommand<string>(async response =>
            {
                var user = new User(command);
                await _userRepository.Add(user);
                response.Data = user.Id;
                response.SetSuccess();
            });
        }
    }
}