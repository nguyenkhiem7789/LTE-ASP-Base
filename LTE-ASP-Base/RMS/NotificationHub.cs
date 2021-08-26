using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LTE_ASP_Base.RMS
{
    public class NotificationHub: Hub
    {
        private readonly INotificationService _notificationService;

        public NotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            Console.WriteLine(connectionId);
            string sessionId = "Nguyen";
            await  _notificationService.Add(connectionId, sessionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _notificationService.Remove(connectionId: Context.ConnectionId);
        }
    }
}