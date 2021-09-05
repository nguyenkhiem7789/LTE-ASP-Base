using System;
using System.Threading.Tasks;
using BaseApplication.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace NotificationManager.Services
{
    public class SignalRHub: Hub
    {
        private readonly ISignalRService _signalRService;
        private readonly IContextService _contextService;

        public SignalRHub(ISignalRService signalRService, IContextService contextService)
        {
            _contextService = contextService;
            _signalRService = signalRService;
        }
        
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            Console.WriteLine(connectionId);
            string sessionId = await _contextService.SessionKeyGet();
            await  _signalRService.Connect(connectionId, sessionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _signalRService.Remove(connectionId: Context.ConnectionId);
        }
    }
}