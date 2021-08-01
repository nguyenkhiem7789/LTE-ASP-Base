using System.Threading.Tasks;
using AccountDomains;
using AccountRepository;

namespace AccountRepositorySQLImplement
{
    public class UserRepository : IUserRepository
    {
        public Task Add(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}