using System;
using System.Threading.Tasks;
using AccountDomains;
using BaseReadModels;

namespace BaseApplication.Interfaces
{
    public interface IContextService
    {
        Task<string> GetIp();
        Task<bool> IsAuthenticated();
        Task<(string, int)> CreateToken(User user, bool remember);
        void LogError(Exception exception, string message);
        Task<string> SessionKeyGet();
        Task SessionKeySet(string sessionId);
    }
}