using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace LTE_ASP_Base.RMS
{
    public class NotificationHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            string sessionId = "Nguyen";
            
            await base.OnConnectedAsync();
        }
    }
}