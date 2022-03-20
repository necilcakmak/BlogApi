using Blog.Api.Filters;
using Blog.Business.Abstract;
using Blog.Core.Utilities;
using Blog.Dto.MainCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class MainCategoryController : ControllerBase
    {
        private readonly IMainCategoryService _mainCategoryService;
        public MainCategoryController(IMainCategoryService mainCategoryService)
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
        public async Task<IActionResult> Update(ParentCategoryAddDto mainCategoryAddDto)
        {
            var res = await _mainCategoryService.Add(mainCategoryAddDto);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
