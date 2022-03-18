using Blog.Api.Filters;
using Blog.Business.Abstract;
using Blog.Core.Utilities;
using Blog.Dto.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
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

        [AuthorizeFilter(RoleTypeEnum.Insert)]
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

        [AuthorizeFilter(RoleTypeEnum.Delete)]
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

        [AuthorizeFilter(RoleTypeEnum.Update)]
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

        [AuthorizeFilter]
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
