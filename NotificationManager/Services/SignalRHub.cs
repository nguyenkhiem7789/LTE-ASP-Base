using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace NotificationManager.Services
{
    public class SignalRHub: Hub
    {
        private readonly ISignalRService _signalRService;

        public SignalRHub(ISignalRService signalRService)
        {
            _signalRService = signalRService;
        }
        
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            Console.WriteLine(connectionId);
            string sessionId = "Nguyen";
            await  _signalRService.Connect(connectionId, sessionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _signalRService.Remove(connectionId: Context.ConnectionId);
        }
    }
}