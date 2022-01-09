using Blog.Business.Abstract;
using Blog.Dto.MainCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
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

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add(MainCategoryAddDto mainCategoryAddDto)
        {
            var res = await _mainCategoryService.Add(mainCategoryAddDto);
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _mainCategoryService.Delete(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update(MainCategoryAddDto mainCategoryAddDto)
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
