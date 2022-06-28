using Blazored.LocalStorage;
using Blog.Ui.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.Ui.Utils
{
    public class AuthStateProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationState anonymous;

        public AuthStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
            anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var apiToken = await localStorage.GetToken();
            if (string.IsNullOrEmpty(apiToken))
                return anonymous;
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(apiToken);

            var cp = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims, "jwtAuthType"));
            return new AuthenticationState(cp);
        }
    }
}
