using BaseDomains;
using EnumDefine;

namespace NotificationDomains
{
    public class NotifyMessage : BaseDomain
    {
        public string[] Conditions { get; set; }
        public NotificationType Type { get; set; }
        public string Value { get; set; }
    }
}