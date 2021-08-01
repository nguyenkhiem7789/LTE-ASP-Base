using System;
using BaseCommands;

namespace AccountCommands
{
    public class AccountBaseCommand : BaseCommand 
    {
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
    }
}