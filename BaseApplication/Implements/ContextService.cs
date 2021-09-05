using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AccountDomains;
using BaseApplication.Interfaces;
using BaseReadModels;
using LTE_ASP_Base.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BaseApplication.Implements
{
    public class ContextService: IContextService
    {

        private readonly AppSettings _appSettings; 
        private const string Lang = "lang";
        public const string SessionCode = "VNNSS";
        private const string Authorization = "Authorization";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextService(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
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
            
        }

        private string _sessionKey;

        public async Task<string> SessionKeyGet()
        {
            if (string.IsNullOrEmpty(_sessionKey))
            {
                _sessionKey = _httpContextAccessor?.HttpContext?.User?.FindFirst(SessionCode)?.Value;
            }

            return await Task.FromResult(_sessionKey);
        }

        public Task SessionKeySet(string sessionId)
        {
            _sessionKey = sessionId;
            return Task.CompletedTask;
        }
    }
}