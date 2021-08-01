﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AccountCommands.Commands;
using AccountCommands.Queries;
using AccountReadModels;
using BaseCommands;
using LTE_ASP_Base.Entities;
using LTE_ASP_Base.Models;

namespace AccountManager.Shared
{
    public interface IAccountService
    {
        Task<BaseCommandResponse<RUser[]>> Gets(AccountGetsQuery query); 
        
        Task<BaseCommandResponse<string>> Add(AccountAddCommand command);
    }
}