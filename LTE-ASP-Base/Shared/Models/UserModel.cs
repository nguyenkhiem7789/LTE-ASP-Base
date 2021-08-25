using EnumDefine;

namespace LTE_ASP_Base.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatusEnum Status { get; set; }
    }
}