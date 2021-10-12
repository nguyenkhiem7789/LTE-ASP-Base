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
    public class RoleService: BaseService, IRoleService
    {
        private IContextService _contextService;
        private IRoleRepository _roleRepository;
        
        protected RoleService(ILogger<BaseService> logger, IContextService contextService, IRoleRepository roleRepository) : base(logger)
        {
            _contextService = contextService;
            _roleRepository = roleRepository;
        }

        public async Task<BaseCommandResponse<string>> Add(RoleAddCommand command)
        {
            return await ProcessCommand<string>(async response =>
            {
                var results = RoleAddValidator.ValidateModel(command);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var role = new Role(command);
                await _roleRepository.Add(role);
                response.Data = role.Id;
                response.SetSuccess();
            });
        }

        public Task<BaseCommandResponse> ActionDefineAdd(ActionDefineAddCommand command)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BaseCommandResponse<RRole>> GetById(RoleGetByIdQuery query)
        {
            return await ProcessCommand<RRole>(async response =>
            {
                var role = await _roleRepository.GetById(query);
                response.Data = role;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<string>> Change(RoleChangeCommand command)
        {
            return await ProcessCommand<string>(async response =>
            {
                var rRole = await _roleRepository.GetById(new RoleGetByIdQuery()
                {
                    Id = command.Id
                });
                var role = new Role(rRole);
                await _roleRepository.Change(role);
                response.Data = role.Id;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RRole[]>> Gets(RoleGetsQuery query)
        {
            return await ProcessCommand<RRole[]>(async response =>
            {
                var roles = await _roleRepository.Gets(query);
                response.Data = roles;
                response.SetSuccess();
            });
        }
    }
}