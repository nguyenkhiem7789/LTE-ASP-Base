using EnumDefine;

namespace LTE_ASP_Base.Models
{
    public class NotificationModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationStatusType Status { get; set; }
    }
}