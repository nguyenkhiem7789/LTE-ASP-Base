using System;
using BaseCommands;
using EnumDefine;

namespace AccountCommands.Queries
{
    public class RoleGetsQuery : BasePagingQuery
    {
        public string Keyword { get; set; }
        public RoleStatusType Status { get; set; }
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
        public override int PageIndex { get; set; }
        public override int PageSize { get; set; }
    }

    public class RoleGetByIdQuery : AccountBaseCommand
    {
        public string Id { get; set; }
    }
}