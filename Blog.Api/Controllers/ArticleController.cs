using Blog.Business.Abstract;
using Blog.Dto.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.APi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _articleService.Get(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
        {
            var res = await _articleService.Add(articleAddDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            var res = await _articleService.GetList();
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
            var res = await _articleService.Delete(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> Update(ArticleAddDto articleAddDto)
        {
            var res = await _articleService.Add(articleAddDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpGet("getlistmyarticles")]
        public async Task<IActionResult> GetListMyArticles()
        {
            var res = await _articleService.GetListMyArticle();
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpGet("DeleteMyArticle")]
        public async Task<IActionResult> DeleteMyArticle(Guid id)
        {
            var res = await _articleService.DeleteMyArticle(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
