using System.Runtime.Serialization;
using AccountReadModels;
using BaseDomains;

namespace AccountDomains
{
    public class User : BaseDomain
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        
        public User(RUser user) : base(user)
        {
            FullName = user.FullName;
            Email = user.FullName;
            PasswordHash = user.PasswordHash;
            PasswordSalt = user.PasswordSalt;
            PhoneNumber = user.PhoneNumber;
        }
    }
}