using Blog.Dto.Token;
using Blog.Entities.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Utilities
{
    public static class TokenHelper
    {

        public static TokenOptions TokenSettings { get; set; } = new TokenOptions();

        public static AccessToken CreateTokenUye(this User user, string redisKey)
        {
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityTokenUye(TokenSettings, user, signingCredentials, redisKey);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = DateTime.Now.AddMinutes(TokenSettings.AccessTokenExpiration),
            };
        }
        private static JwtSecurityToken CreateJwtSecurityTokenUye(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, string redisKey)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: DateTime.Now.AddMinutes(TokenSettings.AccessTokenExpiration),
                notBefore: DateTime.Now,
                claims: [
                    new(type: "RedisKey", value: redisKey) ,
                    new(type: "Role", value: user.RoleName) ,
                ],
                signingCredentials: signingCredentials
                );
            return jwt;
        }
        public static bool ValidateToken(this string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            string token = authToken.Replace("Bearer ", string.Empty);

            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        private static TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = TokenSettings.Issuer,
                ValidAudience = TokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.SecurityKey))
            };
        }
        public static Guid? TokenToRedisId(this string tokenStr)
        {
            try
            {
                var jwt = tokenStr.Replace("Bearer ", string.Empty);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var AuthenticationClaim = token.Claims.ToList();
                string userId = token.Claims.First(x => x.Type == "RedisKey").Value;
                return new Guid(userId);
            }
            catch (Exception)
            {

                return null;
            }

        }

        //private IEnumerable<Claim> SetClaimsUye(User user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()),
        //        new Claim(type: ClaimTypes.Email, value: user.Email.ToString())
        //    };
        //    return claims;
        //}
    }
}
