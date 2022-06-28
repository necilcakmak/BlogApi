using Blog.Core.Results;
using Blog.Dto.Auth;
using Blog.Dto.Token;

namespace Blog.Ui.Services.Auth
{
    public interface IAuthService
    {
        Task<DataResult<AccessToken>> Login(LoginDto loginDto);
    }
}