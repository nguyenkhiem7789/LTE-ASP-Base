﻿using System;
using BaseCommands;

namespace AccountCommands.Commands
{
    public class AccountAddCommand : BaseCommand
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
    }
}