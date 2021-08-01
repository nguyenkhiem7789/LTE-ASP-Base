﻿using System;
using BaseCommands;

namespace AccountCommands.Queries
{
    public class AccountGetsQuery : BasePagingQuery
    {
        public string Keyword { get; set; }
        public override string ObjectId { get; set; }
        public override string ProcessUid { get; set; }
        public override DateTime ProcessDate { get; set; }
        public override string LoginUid { get; set; }
        public override int PageIndex { get; set; }
        public override int PageSize { get; set; }
    }
}