using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<Category>UpdateCategoryAsync(Category category);

        Task<bool> DeleteCategoryAsync(int id);

        Task<Category> GetCategoryByIdAsync(int id);

        Task<decimal> GetCategoryTotalExpensesAsync(int categoryId);

        Task<decimal> GetCategoryBudgetAsync(int categoryId);
    }
}
