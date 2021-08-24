namespace LTE_ASP_Base.Models
{
    public class UserAddRequest
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class UserGetRequest
    {
        public string Keyword { get; set; }
    }

    public class UserChangeRequest
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}