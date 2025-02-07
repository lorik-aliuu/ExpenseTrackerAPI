using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int id);
        Task CreateExpenseAsync(Expense expense);
        Task UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(int id);

        Task<Expense> GetMostExpensiveExpenseAsync();
        Task<Expense> GetLeastExpensiveExpenseAsync();
        Task<decimal> GetAverageDailyExpensesAsync(DateTime fromDate, DateTime toDate);
        Task<decimal> GetAverageMonthlyExpensesAsync(DateTime fromDate, DateTime toDate);
        Task<decimal> GetAverageYearlyExpensesAsync(DateTime fromDate, DateTime toDate);
        Task<decimal> GetTotalExpensesAsync();
    }
}
