using System.Collections.Generic;
using EnumDefine;

namespace BaseReadModels
{
    public class AccountLoginInfo
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public bool IsAdministrator { get; set; }
        public HashSet<string> Permissions { get; set; }
        public string Token { get; set; }
        public string RefToken { get; set; }
        public bool OtpVerify { get; set; }
        public int Version { get; set; }
        
        public string ClientId { get; set; }
        public AccountType AccountType { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        
    }
}