using ExpenseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {

        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense> UpdateExpenseAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if(expense == null)
            {
                return false;
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
