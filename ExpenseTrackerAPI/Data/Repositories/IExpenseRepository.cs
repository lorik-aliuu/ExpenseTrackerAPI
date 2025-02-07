using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Data.Repositories
{
    public interface IExpenseRepository
    {
        Task<Expense> GetExpenseByIdAsync(int id);
        Task<IEnumerable<Expense>> GetAllExpensesAsync();

        Task<Expense> AddExpenseAsync(Expense expense);

        Task<Expense> UpdateExpenseAsync(Expense expense);

        Task<bool> DeleteExpenseAsync(int id);

    }
}
