using System;
using BaseCommands;
using EnumDefine;

namespace NotificationCommands.Commands
{
    public class NotificationAddCommand: BaseCommand
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationStatusType Status { get; set; }
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
    }

    public class NotificationChangeCommand : NotificationAddCommand
    {
        public string Id { get; set; }
    }
}