using System.Data;
using System.Threading.Tasks;
using SystemRepository;
using BaseRepositories;
using Dapper;

namespace SystemRepositorySQLImplement
{
    public class CommonRepository: ICommonRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public CommonRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        
        public Task<long> GetNextValueForSequence(string pathName)
        {
            const string spName = "GetNextValueForSequence";
            return _dbConnectionFactory.WithConnection(p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PathName", pathName, DbType.String);
                return p.ExecuteScalarAsync<long>(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }

        public Task CreateSequence(string pathName)
        {
            const string spName = "CreateSequenceByPathName";
            return _dbConnectionFactory.WithConnection(p =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PathName", pathName, DbType.String);
                return p.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            });
        }
    }
}