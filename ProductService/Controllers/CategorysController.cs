using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Interfaces;
using ProductService.Models;
using ProductService.Repository;
using System.Threading.Tasks;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly ILogger<CategorysController> _logger;
        private readonly CategoryRepository _categoryRepo;
        public CategorysController(ILogger<CategorysController> logger, ICategory category)
        {
            _logger = logger;
            _categoryRepo = (CategoryRepository)category;
        }

        [HttpGet("GetAllCateogory")]
        public async Task<IActionResult> GetAllCateogory()
        {
            var result = await _categoryRepo.GetAllCategories();
            return Ok(result);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var category = await _categoryRepo.GetCategory(categoryId);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] Models.DTOs.CreateCategory createCategory)
        {
            var category = await _categoryRepo.CreateCategory(createCategory);
            return Ok(category);
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var updatedCategory = await _categoryRepo.UpdateCategory(category);
            return Ok(updatedCategory);
        }

        [HttpDelete("DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            var result = await _categoryRepo.DeleteCategory(categoryId);
            if (result == false)
            {
                return NotFound("Category not found.");
            }
            return Ok("Category deleted successfully.");
        }


    }
}
