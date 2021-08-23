using System.Threading.Tasks;
using AccountCommands.Queries;
using AccountDomains;
using AccountReadModels;

namespace AccountRepository
{
    public interface IUserRepository
    {
        Task Add(User user);

        Task<RUser[]> Gets(UserGetsQuery query);
    }
}