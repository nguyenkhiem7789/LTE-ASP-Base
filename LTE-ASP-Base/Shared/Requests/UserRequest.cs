using System.ComponentModel.DataAnnotations;
using EnumDefine;

namespace LTE_ASP_Base.Models
{
    public class UserAddRequest
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserStatusType Status { get; set; }
    }
    
    public class UserChangeRequest
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserStatusType Status { get; set; }
    }

    public class UserGetRequest
    {
        public string Keyword { get; set; }
        public UserStatusType Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class UserGetByIdRequest
    {
        public string Id { get; set; }
    }
}