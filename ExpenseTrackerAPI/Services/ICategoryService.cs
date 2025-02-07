using ExpenseTrackerAPI.Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Services
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto);

        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        Task<CategoryDto> GetCategoryByIdAsync(int id);

        Task<CategoryDto> UpdateCategoryAsync(CategoryDto categoryDto);

        Task<bool> DeleteCategoryAsync(int id);
    }
}
