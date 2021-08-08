using System.Data;
using System.Threading.Tasks;
using AccountDomains;
using AccountRepository;
using BaseRepositories;
using Dapper;

namespace AccountRepositorySQLImplement
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        
        public async Task Add(User user)
        {
            await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FullName", user.FullName);
                parameters.Add("@Email", user.Email);
                parameters.Add("@Password", user.Password);
                var data = connection.Execute("User_Insert", parameters, commandType: CommandType.StoredProcedure);
                return await Task.FromResult(true);
            });
        }
    }
}