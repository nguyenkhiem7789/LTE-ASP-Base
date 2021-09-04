using System;
using BaseReadModels;

namespace NotificationReadModels
{
    public class NotificationBaseReadModel: BaseReadModel
    {
        public override long NumericalOlder { get; set; }
        public override string Id { get; set; }
        public override string Code { get; set; }
        public override string CreatedUid { get; set; }
        public override DateTime CreatedDate { get; set; }
        public override DateTime CreatedDateUtc { get; set; }
        public override string UpdatedUid { get; set; }
        public override DateTime UpdatedDate { get; set; }
        public override DateTime UpdatedDateUtc { get; set; }
        public override int Version { get; set; }
        public override string LoginUid { get; set; }
        public override int TotalRow { get; set; }
    }
}