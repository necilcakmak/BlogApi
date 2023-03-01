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
        public async Task<IActionResult> UpdateMyInformation([FromForm] UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto.ImageFile != null)
            {
                userUpdateDto.ImageName = await SaveImage(userUpdateDto.ImageFile, userUpdateDto.ImageName);
            }
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
            var res = await _userService.UserInformation() as DataResult<UserDto>;
            if (!res.Success)
            {
                return BadRequest(res);
            }
            res.Data.ImageSrc = String.Format("{0}://{1}{2}/images/users/{3}", Request.Scheme, Request.Host, Request.PathBase, res.Data.ImageName);

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

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile, string deletedName)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yyyymmssfff") + Path.GetExtension(imageFile.FileName);
            string FilePath = _environment.WebRootPath + "\\images\\users";
            if (!System.IO.File.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string imagePath = FilePath + "\\" + imageName;
            if (System.IO.File.Exists(imagePath) && deletedName != "DefaultUser.jpg")
            {
                System.IO.File.Delete(deletedName);
            }
            using FileStream stream = System.IO.File.Create(imagePath);
            await imageFile.CopyToAsync(stream);
            return imageName;
        }
    }

}


