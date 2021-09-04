using System.Threading.Tasks;
using BaseCommands;
using NotificationCommands.Commands;
using NotificationCommands.Queries;
using NotificationReadModels;

namespace NotificationManager.Shared
{
    public interface INotificationService
    {
        Task<BaseCommandResponse<RNotification[]>> Gets(NotificationGetsQuery query);
        Task<BaseCommandResponse<RNotification>> GetById(NotificationGetByIdQuery query);
        Task<BaseCommandResponse<string>> Add(NotificationAddCommand command);
        Task<BaseCommandResponse> Change(NotificationChangeCommand command);
    }
}