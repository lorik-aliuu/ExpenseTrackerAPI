using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public interface IExpenseService
    {
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<IEnumerable<Expense>> GetExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int id);
        Task<Expense> UpdateExpenseAsync(Expense expense);

        Task<bool> DeleteExpenseAsync(int id);

        Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(int categoryId);

        Task<decimal> GetTotalExpensesAsync();

        Task<decimal> GetAverageDailyExpensesAsync();

        Task<decimal> GetAverageMonthlyExpensesAsync();

        Task<decimal> GetAverageYearlyExpensesAsync();


    }
}
