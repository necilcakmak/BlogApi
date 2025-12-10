using Asp.Versioning;
using Blog.Business.Abstract;
using Blog.Core.Results;
using Blog.Dto.Auth;
using Blog.Dto.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Blog.APi.Controllers
{
    [EnableRateLimiting("Api")]
    [Route("api/[controller]")]
    [ApiVersion(1.0)]
    [ApiVersion(1.1)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [MapToApiVersion("1.0")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var res = await _authService.Login(loginDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            var token = res as DataResult<AccessToken>;
            Response.Cookies.Append("authToken", token.Data.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,                // HTTPS için true
                SameSite = SameSiteMode.None, // Cross-origin izin
                Expires = token.Data.Expiration,
                Path = "/"
            });

            return Ok(res);
        }

        //[MapToApiVersion("1.1")]
        //[HttpPost("login")]
        //public IActionResult LoginV2(LoginDto loginDto)
        //{
        //    return Ok("login test V2");
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var res = await _authService.Register(registerDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }

            return Ok(res);
        }

        [HttpGet("accountconfirm/{id}")]
        public async Task<IActionResult> AccountConfirm(string id)
        {
            Guid guid = new(id);
            var res = await _authService.AccountConfirm(guid);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
