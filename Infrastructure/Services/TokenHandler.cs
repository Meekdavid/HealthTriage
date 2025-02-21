using Common.ConfigurationSettings;
using Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Persistence.DBModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenHandler
    {
        public IConfiguration _configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<Token> CreateAccessTokenAsync(AppUser user, List<string> roles)
        {
            Token token = new Token();

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettings.ApplicationSetting.JwtSecret));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddMinutes(60);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: ConfigSettings.ApplicationSetting.BaseLocalStorageDomain,
                audience: ConfigSettings.ApplicationSetting.BaseLocalStorageDomain,
                expires: token.Expiration,//Token expiration date
                notBefore: DateTime.UtcNow,//Set how long it takes for the token to be activated after it is produced.
                signingCredentials: signingCredentials,
                claims: claims
            );

            //Create token
            string str = ConfigSettings.ApplicationSetting.JwtSecret;
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            //Create refresh token
            token.RefreshToken = CreateRefreshToken();
            return Task.FromResult<Token>(token);
        }

        //Refresh token creator
        private string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
                random.GetBytes(number);
            return Convert.ToBase64String(number);

        }

    }
}
