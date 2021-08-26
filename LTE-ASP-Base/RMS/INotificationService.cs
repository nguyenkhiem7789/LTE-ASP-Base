using System.Threading.Tasks;

namespace LTE_ASP_Base.RMS
{
    public interface INotificationService
    {
        Task SendMessage(string message);
        Task Add(string connectionId, string sessionId);
        Task Remove(string connectionId);
    }
}