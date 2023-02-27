using Blog.Api.Filters;
using Blog.Business.Abstract;
using Blog.Core.Results;
using Blog.Core.Utilities;
using Blog.Dto.Auth;
using Blog.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

namespace Blog.APi.Controllers
{
    [EnableRateLimiting("Api")]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;
        public UserController(IUserService userService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _environment = environment;
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
        [HttpPost("updateuserimage")]
        public async Task<IActionResult> UpdateUserImage()
        {
            var item = Request.Form.Files[0];
            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            string userName = dict["userName"];
            string FileName = item.FileName;
            string FilePath = _environment.WebRootPath + "\\images\\users";
            if (!System.IO.File.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string imagePath = FilePath + "\\" + userName + FileName;
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            using FileStream stream = System.IO.File.Create(imagePath);
            await item.CopyToAsync(stream);
            var res = await _userService.UpdateUserImage(userName, FileName);

            return Ok(res);
        }


        [NonAction]
        private string GetFilePath()
        {
            return _environment.WebRootPath + "\\images\\users";
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

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            var res = await _userService.GetList();
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


