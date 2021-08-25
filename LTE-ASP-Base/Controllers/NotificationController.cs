using System;
using System.Threading.Tasks;
using BaseApplication.Controllers;
using LTE_ASP_Base.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LTE_ASP_Base.RMS
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController: BaseApiController
    {
        private readonly IHubContext<NotificationHub> _notificationHub;
        public NotificationController(IHttpContextAccessor httpContextAccessor, IHubContext<NotificationHub> notificationHub) : base(httpContextAccessor)
        {
            _notificationHub = notificationHub;
        }

        [HttpPost("Test")]
        public async Task<string> Test([FromBody] NotificationRequest request)
        {
            var retMessage = string.Empty;
            try
            {
                await _notificationHub.Clients.All.SendAsync("Notify", "this is notify test");
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }
}