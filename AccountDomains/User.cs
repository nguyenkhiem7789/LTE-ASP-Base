using System.Runtime.Serialization;
using System.Text;
using AccountCommands.Commands;
using AccountReadModels;
using BaseDomains;
using Common;
using EnumDefine;
using Microsoft.Extensions.Primitives;

namespace AccountDomains
{
    public class User : BaseDomain
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatusType Status {get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        
        public User(RUser user) : base(user)
        {
            FullName = user.FullName;
            Email = user.FullName;
            Status = user.Status;
        }

        public User(UserAddCommand command) : base(command)
        {
            Code = command.Code;
            FullName = command.FullName;
            Email = command.Email;
            Status = command.Status;
            Password = command.Password;
            PasswordHash = EncryptionExtensions.Encryption(Code, command.Password, out string salf);
            PasswordSalt = salf;
        }

        public void Change(UserChangeCommand command)
        {
            Code = command.Id ?? command.Id;
            FullName = command.FullName ?? command.FullName;
            Email = command.Email ?? command.Email;
            Status = command.Status;
        }
    }
}