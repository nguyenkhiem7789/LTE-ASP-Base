using System;
using BaseCommands;

namespace NotificationCommands.Queries
{
    public class NotificationGetsQuery: BasePagingQuery
    {
        public string keyword { get; set; }
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
        public override int PageIndex { get; set; }
        public override int PageSize { get; set; }
    }

    public class NotificationGetByIdQuery: NotificationBaseCommand
    {
        public string Id { get; set; }
    }
    
}