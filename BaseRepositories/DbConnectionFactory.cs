using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BaseRepositories
{
    public class DbConnectionFactory: IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<IDbConnection> GetNewConnectionAsync(string connectionString)
        {
            try
            {
                DbConnection dbConnection = new SqlConnection(connectionString);
                await dbConnection.OpenAsync();
                return dbConnection;
            }
            catch (Exception e)
            {
                e.Data["BaseDao.Message-CreateDbConnection"] = "Not new SqlConnection";
                e.Data["BaseDao.ConnectionString"] = connectionString;
                throw e;
            }
        }
        
        public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using var dbConnection = await GetNewConnectionAsync(_connectionString);
                return await getData(dbConnection);
            }
            catch (TimeoutException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute TimeoutException";
                ex.Data["BaseDao.ConnectionString"] = _connectionString;
                throw ex;
            }
            catch (SqlException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute SqlException";
                ex.Data["BaseDao.ConnectionString"] = _connectionString;
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Exception";
                ex.Data["BaseDao.ConnectionString"] = _connectionString;
                throw ex;
            }
        }

        public async Task<T> WithConnection<T>(Func<IDbConnection, IDbTransaction, Task<T>> getData)
        {
            try
            {
                using var dbConnection = await GetNewConnectionAsync(_connectionString);
                using IDbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    var result = await getData(dbConnection, transaction);
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction Exception";
                    ex.Data["BaseDao.ConnectionString"] = _connectionString;
                    throw;
                }
            }
            catch (TimeoutException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction TimeoutException";
                ex.Data["BaseDao.ConnectionString"] = _connectionString;
                throw ex;
            }
            catch (SqlException ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction SqlException";
                ex.Data["BaseDao.ConnectionString"] = _connectionString;
                throw ex;
            }
            catch (Exception ex)
            {
                ex.Data["BaseDao.Message-WithConnection"] = "Excute Transaction Exception";
                ex.Data["BaseDao.ConnectionString"] = _connectionString;
                throw ex;
            }
        }
    }
}