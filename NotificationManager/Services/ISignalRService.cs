using System.Threading.Tasks;

namespace NotificationManager.Services
{
    public interface ISignalRService
    {
        Task SendMessage(string message);
        Task Connect(string connectionId, string sessionId);
        Task Remove(string connectionId);
    }
}