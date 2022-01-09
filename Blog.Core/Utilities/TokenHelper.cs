using Blog.Dto.Token;
using Blog.Entities.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Blog.Core.Utilities
{
    public class TokenHelper : ITokenHelper
    {
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public TokenHelper(IOptions<TokenOptions> options)
        {
            _tokenOptions = options.Value;
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }
        public AccessToken CreateTokenUye(User user)
        {
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, algorithm: SecurityAlgorithms.HmacSha256Signature);
            var jwt = CreateJwtSecurityTokenUye(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
            };
        }

        public JwtSecurityToken CreateJwtSecurityTokenUye(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaimsUye(user),
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        public IEnumerable<Claim> SetClaimsUye(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()),
                new Claim(type: ClaimTypes.Email, value: user.Email.ToString())
            };
            return claims;
        }
        public bool ValidateToken(string authToken)
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
        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey))
            };
        }

        public Guid TokenToUserId(string tokenStr)
        {
            var jwt = tokenStr.Replace("Bearer ", string.Empty);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var AuthenticationClaim = token.Claims.ToList();
            string userId = token.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return new Guid(userId);
        }
    }
}
