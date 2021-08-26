﻿using System;
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
        private readonly INotificationService _notificationService;
        
        public NotificationController(IHttpContextAccessor httpContextAccessor, INotificationService notificationService) : base(httpContextAccessor)
        {
            _notificationService = notificationService;
        }

        [HttpPost("Test")]
        public async Task<string> Test([FromBody] NotificationRequest request)
        {
            var retMessage = string.Empty;
            try
            {
                await _notificationService.SendMessage("this is tessst");
                retMessage = "success";
            }
            catch (Exception e)
            {
                retMessage = e.ToString();
            }

            return retMessage;
        }
    }
}