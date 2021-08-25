using System;
using BaseCommands;
using EnumDefine;

namespace AccountCommands.Commands
{
    public class UserAddCommand : BaseCommand
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatusEnum Status { get; set; }
        public string Password { get; set; }
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
    }

    public class UserChangeCommand : UserAddCommand
    {
        public string Id { get; set; }
    }
}