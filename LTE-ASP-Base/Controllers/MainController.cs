using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using LTE_ASP_Base.Models;
using Microsoft.AspNetCore.Authorization;

namespace LTE_ASP_Base.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController: ControllerBase
    {
        [HttpGet("people/all")]
        public ActionResult<IEnumerable<Person>> getAll()
        {
            return new []
            {
                new Person { Name = "Ana" },
                new Person { Name = "Felipe" },
                new Person { Name = "Emillia" }
            };
        }
        
        [HttpGet("gettoken")]
        public Object GetToken()
        {
            string key = "my_secret_key_2021";
            var issuer = "http://mysite.com";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            //Create a List of Claims, Keep claims name short
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));
            
            //Create Security Token object by giving required paremeters
            var token = new JwtSecurityToken(issuer, issuer, permClaims, expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new {data = jwt_token};
        }
        
        [HttpPost("getname1")]  
        public String GetName1() {  
            if (User.Identity.IsAuthenticated) {  
                var identity = User.Identity as ClaimsIdentity;  
                if (identity != null) {  
                    IEnumerable < Claim > claims = identity.Claims;  
                }  
                return "Valid";  
            } else {  
                return "Invalid";  
            }  
        }  
      
        [Authorize]  
        [HttpPost("getname2")]  
        public Object GetName2() {  
            var identity = User.Identity as ClaimsIdentity;  
            if (identity != null) {  
                IEnumerable < Claim > claims = identity.Claims;  
                var name = claims.Where(p => p.Type == "name").FirstOrDefault() ? .Value;  
                return new {  
                    data = name  
                };  
      
            }  
            return null;  
        } 

    }
}