using System.Threading.Tasks;
using AccountCommands.Queries;
using AccountDomains;
using AccountReadModels;

namespace AccountRepository
{
    public interface IUserRepository
    {
        Task<RUser[]> Gets(UserGetsQuery query);
        Task<RUser> GetById(UserGetByIdQuery query);
        Task<RUser> GetByUserName(AuthenticateQuery query);
        Task Add(User user);
        Task Change(User user);

    }
}