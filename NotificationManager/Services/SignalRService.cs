using System.Threading.Tasks;
using EnumDefine;
using Microsoft.AspNetCore.SignalR;

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

        public async Task SendMessage(string message)
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
                    await _notificationHub.Clients.Client(_connectionId).SendAsync("Notify", message);
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