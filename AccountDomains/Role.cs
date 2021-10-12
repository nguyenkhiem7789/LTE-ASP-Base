using AccountCommands.Commands;
using AccountReadModels;
using BaseDomains;
using EnumDefine;

namespace AccountDomains
{
    public class Role: BaseDomain
    {
        public string Name { get; set; }
        public RoleStatusType Status { get; set; }

        public Role(RRole role) : base(role)
        {
            Name = role.Name;
            Status = role.Status;
        }

        public Role(RoleAddCommand command) : base(command)
        {
            Code = command.Code;
            Name = command.Name;
            Status = command.Status;
        }

        public void Change(RoleChangeCommand command)
        {
            Name = command.Name ?? command.Name;
            Status = command.Status;
        }
    }
}