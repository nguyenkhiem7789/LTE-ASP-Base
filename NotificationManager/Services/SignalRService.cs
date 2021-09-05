using System.Linq;
using System.Threading.Tasks;
using EnumDefine;
using Microsoft.AspNetCore.SignalR;
using NotificationDomains;

namespace NotificationManager.Services
{
    public class SignalRService: ISignalRService
    { 
        private readonly IHubContext<SignalRHub> _notificationHub;

        private string _connectionId;

        private NotificationType type = NotificationType.CLIENT;

        public SignalRService(IHubContext<SignalRHub> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public async Task SendMessage(NotifyMessage message)
        {
            switch (type)
            {
                case NotificationType.All: 
                    await _notificationHub.Clients.All.SendAsync("Notify", message);
                    break;
                case NotificationType.GROUP: 
                    await _notificationHub.Clients.Group("Group").SendAsync("Notify", message);
                    break;
                case NotificationType.CLIENT:
                    await _notificationHub.Clients.Clients(message.Conditions.Distinct().ToArray()).SendAsync("Notify", message);
                    break;
            }
        }

        public async Task Connect(string connectionId, string sessionId)
        {
            _connectionId = connectionId;
            await _notificationHub.Clients.Client(connectionId).SendAsync("OnConnectSuccess", "Connect Signal R success!");
        }

        public Task Remove(string connectionId)
        {
            throw new System.NotImplementedException();
        }
    }
}