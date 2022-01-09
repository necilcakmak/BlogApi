using Blog.Dto.Token;
using Blog.Entities.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Utilities
{
    public interface ITokenHelper
    {
        AccessToken CreateTokenUye(User user);
        JwtSecurityToken CreateJwtSecurityTokenUye(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials);
        IEnumerable<Claim> SetClaimsUye(User user);
        bool ValidateToken(string authToken);
        TokenValidationParameters GetValidationParameters();
        Guid TokenToUserId(string tokenStr);
    }
}
