using System.Threading.Tasks;
using NotificationCommands.Queries;
using NotificationDomains;
using NotificationReadModels;

namespace NotificationRepository
{
    public interface INotificationRepository
    {
        Task<RNotification[]> Gets(NotificationGetsQuery query);
        Task<RNotification> GetById(NotificationGetByIdQuery query);
        Task Add(Notification notification);
        Task Change(Notification notification);
    }
}