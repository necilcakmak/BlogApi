using Blog.Core.Results;
using Blog.Dto.Auth;
using Blog.Dto.Token;

namespace Blog.Ui.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientService client;

        public AuthService(IHttpClientService httpClientService)
        {
            client = httpClientService;
        }

        public async Task<DataResult<AccessToken>> Login(LoginDto loginDto)
        {
            return await client.Post<DataResult<AccessToken>, LoginDto>("auth/login", loginDto);
        }
    }
}
