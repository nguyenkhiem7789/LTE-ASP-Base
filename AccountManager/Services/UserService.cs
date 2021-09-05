using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
using LTE_ASP_Base.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AccountManager
{
    public class UserService : BaseService, IUserService
    {
        private IContextService _contextService;
        
        private IUserRepository _userRepository;
        
        public UserService(IContextService contextService, ILogger<BaseService> logger, IUserRepository userRepository) : base(/*contextService, */logger)
        {
            _contextService = contextService;
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

        public async Task<BaseCommandResponse<RUser[]>> GetAll()
        {
            return await ProcessCommand<RUser[]>(async response =>
            {
                var users = await _userRepository.GetAll();
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

        public async Task<BaseCommandResponse<RLoginModel>> Authenticate(AuthenticateQuery query)
        {
            return await ProcessCommand<RLoginModel>(async response =>
            {
                var rUser = await _userRepository.GetByUserName(query: query);
                if (rUser == null)
                {
                    response.SetFail("User not exist!");
                    return;
                }
                var user = new User(rUser);
                if (!user.ComparePassword(query.Password))
                {
                    response.SetFail("Username or password is not exactly!");
                    return;
                }
                var tokenInfo = await _contextService.CreateToken(user, query.Remember);
                response.Data = new RLoginModel()
                {
                    token = tokenInfo.Item1,
                    minuteExpire = tokenInfo.Item2
                };
                response.SetSuccess();
            });
        }
        
    }
}