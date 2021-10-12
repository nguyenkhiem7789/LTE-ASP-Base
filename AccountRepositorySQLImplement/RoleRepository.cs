using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AccountCommands.Queries;
using AccountDomains;
using AccountReadModels;
using AccountRepository;
using BaseRepositories;
using Dapper;

namespace AccountRepositorySQLImplement
{
    public class RoleRepository: IRoleRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RoleRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<RRole[]> Gets(RoleGetsQuery query)
        {
            return await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", query.Keyword);
                parameters.Add("@Status", query.Status);
                var data = await connection.QueryAsync<RRole>("[Role_Gets]", parameters,
                    commandType: CommandType.StoredProcedure);
                var roles = data.ToArray();
                return roles;
            });
        }

        public async Task<RRole> GetById(RoleGetByIdQuery query)
        {
            return await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", query.Id);
                var data = await connection.QueryFirstOrDefaultAsync<RRole>("[Role_GetById]", parameters,
                    commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task Add(Role role)
        {
            await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", role.Id);
                parameters.Add("@Name", role.Name);
                parameters.Add("@Status", role.Status);
                parameters.Add("@CreatedDate", role.CreatedDate);
                parameters.Add("@CreatedDateUtc", role.CreateDateUtc);
                parameters.Add("@CreatedUid", role.CreatedUid);
                parameters.Add("@Code", role.Code);
                var data = connection.Execute("[Role_Insert]", parameters, commandType: CommandType.StoredProcedure);
                return await Task.FromResult(true);
            });
        }

        public async Task Change(Role role)
        {
            await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", role.Id);
                parameters.Add("@Name", role.Name);
                parameters.Add("@Status", role.Status);
                var data = connection.Execute("[Role_Update]", parameters, commandType: CommandType.StoredProcedure);
                return await Task.FromResult(true);
            });
        }
    }
}