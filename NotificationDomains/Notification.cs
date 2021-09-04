using BaseDomains;
using NotificationCommands.Commands;
using NotificationReadModels;

namespace NotificationDomains
{
    public class Notification : BaseDomain
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Notification(RNotification notification) : base(notification)
        {
            Title = notification.Title;
            Content = notification.Content;
        }

        public Notification(NotificationAddCommand command) : base(command)
        {
            Code = command.Code;
            Title = command.Title;
            Content = command.Content;
        }

        public void Change(NotificationChangeCommand command)
        {
            Code = command.Id ?? command.Id;
            Title = command.Title ?? command.Title;
            Content = command.Content ?? command.Content;
        }
    }
}