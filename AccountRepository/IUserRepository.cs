using System.Threading.Tasks;
using AccountDomains;

namespace AccountRepository
{
    public interface IUserRepository
    {
        Task Add(User user);
    }
}