using Blog.Core.Results;
using Blog.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Abstract
{
    public interface IAuthService
    {
        Task<Result> Login(LoginDto loginDto);
        Task<Result> Register(RegisterDto registerDto);
    }
}
