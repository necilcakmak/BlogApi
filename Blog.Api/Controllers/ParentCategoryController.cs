using Asp.Versioning;
using Blog.Api.Filters;
using Blog.Business.Abstract;
using Blog.Dto.ParentCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Blog.Api.Controllers
{
    [EnableRateLimiting("Api")]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion(1.0)]
    public class ParentCategoryController : ControllerBase
    {
        private readonly IParentCategoryService _mainCategoryService;
        public ParentCategoryController(IParentCategoryService mainCategoryService)
        {
            _mainCategoryService = mainCategoryService;
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _mainCategoryService.Get(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [AuthorizeFilter("Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> Add(ParentCategoryAddDto parentCategoryAddDto)
        {
            var res = await _mainCategoryService.Add(parentCategoryAddDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            var res = await _mainCategoryService.GetList();
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
            var res = await _mainCategoryService.Delete(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }


        [AuthorizeFilter("Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> Update(ParentCategoryUpdateDto mainCategoryAddDto)
        {
            var res = await _mainCategoryService.Update(mainCategoryAddDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
