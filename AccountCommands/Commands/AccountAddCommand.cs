using System;
using BaseCommands;

namespace AccountCommands.Commands
{
    public class AccountAddCommand : BaseCommand
    {
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
    }
}