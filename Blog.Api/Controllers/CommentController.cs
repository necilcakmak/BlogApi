using Blog.Business.Abstract;
using Blog.Dto.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _commentService.Get(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add(CommentAddDto commentAddDto)
        {
            var res = await _commentService.Add(commentAddDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            var res = await _commentService.GetList();
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _commentService.Delete(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(CommentAddDto commentAddDto)
        {
            var res = await _commentService.Add(commentAddDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpDelete("deletemycomment")]
        public async Task<IActionResult> DeleteMyComment(Guid id)
        {
            var res = await _commentService.DeleteMyComment(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
