using System.Threading.Tasks;
using NotificationDomains;

namespace NotificationManager.Services
{
    public interface ISignalRService
    {
        Task SendMessage(NotifyMessage message);
        Task Connect(string connectionId, string sessionId);
        Task Remove(string connectionId);
    }
}