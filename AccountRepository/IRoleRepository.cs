using System.Threading.Tasks;
using AccountCommands.Queries;
using AccountDomains;
using AccountReadModels;

namespace AccountRepository
{
    public interface IRoleRepository
    {
        Task<RRole[]> Gets(RoleGetsQuery query);
        Task<RRole> GetById(RoleGetByIdQuery query);
        Task Add(Role role);
        Task Change(Role role);
    }
}