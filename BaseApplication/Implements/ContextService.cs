using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AccountDomains;
using BaseApplication.Interfaces;
using BaseReadModels;
using LTE_ASP_Base.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BaseApplication.Implements
{
    public class ContextService: IContextService
    {

        private readonly AppSettings _appSettings; 

        public ContextService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public Task<string> GetIp()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAuthenticated()
        {
            throw new NotImplementedException();
        }

        public async Task<(string, int)> CreateToken(User user, bool remember)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var uniqueNameKey = JwtRegisteredClaimNames.UniqueName;
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            int minuteExpire = remember ? _appSettings.LoginExpiresTime + 60 : _appSettings.LoginExpiresTime;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim(uniqueNameKey, user.FullName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(minuteExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenValue = tokenHandler.WriteToken(token);
            return (tokenValue, minuteExpire);
        }

        public void LogError(Exception exception, string message)
        {
            throw new NotImplementedException();
        }
    }
}