using System;
using System.Linq;
using System.Threading.Tasks;
using BaseApplication.Implements;
using BaseCommands;
using Hangfire;
using Microsoft.Extensions.Logging;
using NotificationCommands.Commands;
using NotificationCommands.Queries;
using NotificationDomains;
using NotificationManager.Shared;
using NotificationManager.Validations;
using NotificationReadModels;
using NotificationRepository;

namespace NotificationManager.Services
{
    public class NotificationService: BaseService, INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        private readonly IRecurringJobManager _recurringJobManager;

        public NotificationService(INotificationRepository notificationRepository, ILogger<BaseService> logger, IRecurringJobManager recurringJobManager) : base(logger)
        {
            _notificationRepository = notificationRepository;
            _recurringJobManager = recurringJobManager;
        }
        
        public async Task<BaseCommandResponse<RNotification[]>> Gets(NotificationGetsQuery query)
        {
            return await ProcessCommand<RNotification[]>(async response =>
            {
                var notifications = await _notificationRepository.Gets(query: query);
                response.Data = notifications;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<RNotification>> GetById(NotificationGetByIdQuery query)
        {
            return await ProcessCommand<RNotification>(async response =>
            {
                var notification = await _notificationRepository.GetById(query: query);
                response.Data = notification;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse<string>> Add(NotificationAddCommand command)
        {
            return await ProcessCommand<string>(async response =>
            {
                var results = NotificationAddValidator.ValidateModel(command);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }
                var notification = new Notification(command);
                await _notificationRepository.Add(notification);
                response.Data = notification.Id;
                response.SetSuccess();
            });
        }

        public async Task<BaseCommandResponse> Change(NotificationChangeCommand command)
        {
            return await ProcessCommand(async response =>
            {
                var results = NotificationChangeValidator.ValidateModel(command);
                if (!results.IsValid)
                {
                    response.SetFail(results.Errors.Select(p => p.ToString()));
                    return;
                }

                var rNotification = await _notificationRepository.GetById(new NotificationGetByIdQuery()
                {
                    Id = command.Id
                });
                var notification = new Notification(rNotification);
                notification.Change(command: command);
                await _notificationRepository.Change(notification);
                // Hangfire - Schedule send notification each second
                _recurringJobManager.AddOrUpdate($"Notification_{command.Id}", () => Test(), "* * * * *");
                response.SetSuccess();
            });
        }

        public void Test()
        {
            Console.WriteLine("This is test");
        }
    }
}