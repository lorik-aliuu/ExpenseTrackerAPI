using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<Category> AddCategoryAsync(Category category);

        Task<Category> UpdateCategoryAsync(Category category);

        Task<bool> DeleteCategoryAsync(int id);

    }
}
