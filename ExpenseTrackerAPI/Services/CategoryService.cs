using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IExpenseRepository _expenseRepository;

        public CategoryService(ICategoryRepository categoryRepository, IExpenseRepository expenseRepository)
        {
            _categoryRepository = categoryRepository;
            _expenseRepository = expenseRepository;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            return await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(category.Id);
            if (existingCategory == null)
                throw new KeyNotFoundException("Kategoria nuk u gjet.");

            return await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
                throw new KeyNotFoundException("Kategoria nuk u gjet.");

            return await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<decimal> GetCategoryTotalExpensesAsync(int categoryId)
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            return expenses.Where(e => e.CategoryId == categoryId).Sum(e => e.Amount);
        }

        public async Task<decimal> GetCategoryBudgetAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException("Kategoria nuk ekziston.");

            return category.Budget;
        }
    }
}
