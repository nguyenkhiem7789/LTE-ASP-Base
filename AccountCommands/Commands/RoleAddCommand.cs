using System;
using EnumDefine;

namespace AccountCommands.Commands
{
    public class RoleAddCommand: AccountBaseCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public RoleStatusType Status { get; set; }
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
    }

    public class RoleChangeCommand : RoleAddCommand
    {
        public string Id { get; set; }
    }
}