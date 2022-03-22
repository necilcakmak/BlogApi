using Blog.Api.Filters;
using Blog.Business.Abstract;
using Blog.Core.Utilities;
using Blog.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AuthorizeFilter]
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

        [AuthorizeFilter]
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

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _userService.Get(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }


        [AuthorizeFilter("Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _userService.Delete(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [AuthorizeFilter("Admin")]
        [HttpGet("sendnewpostmail")]
        public async Task<IActionResult> SendNewPostMail()
        {
            var res = await _userService.SendNewPostMail();
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
