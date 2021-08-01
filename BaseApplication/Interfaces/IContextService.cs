using System;
using System.Threading.Tasks;
using BaseReadModels;

namespace BaseApplication.Interfaces
{
    public interface IContextService
    {
        Task<string> GetIp();
        Task<bool> IsAuthenticated();
        Task<(string, int)> CreateToken(string userName, bool remember, AccountLoginInfo accountLoginInfo);
        void LogError(Exception exception, string message);
    }
}