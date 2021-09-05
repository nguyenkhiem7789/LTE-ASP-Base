using EnumDefine;

namespace NotificationReadModels
{
    public class RNotification: NotificationBaseReadModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationStatusType Status { get; set; }
    }
}