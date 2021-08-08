using System.ComponentModel.DataAnnotations;

namespace LTE_ASP_Base.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
    
    
}