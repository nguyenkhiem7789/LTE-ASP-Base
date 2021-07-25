using System;
using System.Data;
using System.Threading.Tasks;

namespace BaseRepositories
{
    public interface IDbConnectionFactory
    {
        Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData);
        Task<T> WithConnection<T>(Func<IDbConnection, IDbTransaction, Task<T>> getData);
    }
}