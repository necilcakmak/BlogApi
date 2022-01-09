using Blog.Business.Abstract;
using Blog.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPut("updatemyinformation")]
        public async Task<IActionResult> UpdateMyInformation(UserUpdateDto userUpdateDto)
        {
            var res = await _userService.UpdateMyInformation(userUpdateDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpGet("getmyinformation")]
        public async Task<IActionResult> UserInformation()
        {
            var res = await _userService.UserInformation();
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
