using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services.Dtos;

namespace ExpenseTrackerAPI.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDTO>> GetAllExpensesAsync();
        Task<ExpenseDTO> GetExpenseByIdAsync(int id);
        Task CreateExpenseAsync(ExpenseDTO expense);
        Task UpdateExpenseAsync(ExpenseDTO expense);
        Task DeleteExpenseAsync(int id);

        Task<ExpenseDTO> GetMostExpensiveExpenseAsync();
        Task<ExpenseDTO> GetLeastExpensiveExpenseAsync();
        Task<decimal> GetAverageDailyExpensesAsync(DateTime fromDate, DateTime toDate);
        Task<decimal> GetAverageMonthlyExpensesAsync(DateTime fromDate, DateTime toDate);
        Task<decimal> GetAverageYearlyExpensesAsync(DateTime fromDate, DateTime toDate);
        Task<decimal> GetTotalExpensesAsync();

        Task<UserDto> GetUserWithHighestTotalExpensesAsync();

        Task<CategoryDto> GetMostFrequentlyUsedCategoryAsync();
    }
}
