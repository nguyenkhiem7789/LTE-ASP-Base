using EnumDefine;

namespace LTE_ASP_Base.Models
{
    public class NotificationAddRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationStatusType Status { get; set; }
    }

    public class NotificationChangeRequest
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationStatusType Status { get; set; }
    }

    public class NotificationGetsRequest
    {
        public string Keyword { get; set; }
    }

    public class NotificationGetByIdRequest
    {
        public string Id { get; set; }
    }

    public class NotificationSendMessageRequest
    {
        public string[] Conditions { get; set; }
        public NotificationType Type { get; set; }
        public string Value { get; set; } 
    }
}