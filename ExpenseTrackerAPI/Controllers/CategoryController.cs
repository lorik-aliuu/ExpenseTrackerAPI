using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

 
    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest("Category data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);

        return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
    }

  
    [HttpGet("getbyid")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

  
    [HttpGet("getall")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }


    [HttpPut("updatebyid")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
    {

        var updatedCategory = await _categoryService.UpdateCategoryAsync(categoryDto);
        return Ok(updatedCategory);
    }

   
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute]int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        var isDeleted = await _categoryService.DeleteCategoryAsync(id);
        if (isDeleted)
        {
            return NoContent();
        }

        return BadRequest("Unable to delete category.");
    }
}
