using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public ExpenseService(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _expenseRepository.GetAllExpensesAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            return await _expenseRepository.GetExpenseByIdAsync(id);
        }

        public async Task CreateExpenseAsync(Expense expense)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(expense.CategoryId);
            if (category == null)
                throw new ArgumentException("Kategoria nuk ekziston.");

            var totalCategoryExpenses = (await _expenseRepository.GetAllExpensesAsync())
                                        .Where(e => e.CategoryId == expense.CategoryId)
                                        .Sum(e => e.Amount);

            if (totalCategoryExpenses + expense.Amount > category.Budget)
                throw new InvalidOperationException("Shpenzimi tejkalon buxhetin e kategorisë.");

            var user = await _userRepository.GetUserByIdAsync(expense.UserId);
            if (user == null)
                throw new ArgumentException("Përdoruesi nuk ekziston.");

            var totalUserExpenses = (await _expenseRepository.GetAllExpensesAsync())
                                     .Where(e => e.UserId == expense.UserId)
                                     .Sum(e => e.Amount);

            if (totalUserExpenses + expense.Amount > user.OverAllBudget)
                throw new InvalidOperationException("Shpenzimi tejkalon buxhetin total të përdoruesit.");

            if (expense.Date == default(DateTime))
                expense.Date = DateTime.Now;

            await _expenseRepository.AddExpenseAsync(expense);
        }

        public async Task UpdateExpenseAsync(Expense expense)
        {
            var existingExpense = await _expenseRepository.GetExpenseByIdAsync(expense.Id);
            if (existingExpense == null)
                throw new KeyNotFoundException("Shpenzimi nuk u gjet.");

            await _expenseRepository.UpdateExpenseAsync(expense);
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(id);
            if (expense == null)
                throw new KeyNotFoundException("Shpenzimi nuk u gjet.");

            await _expenseRepository.DeleteExpenseAsync(id);
        }

        public async Task<Expense> GetMostExpensiveExpenseAsync()
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            return expenses.OrderByDescending(e => e.Amount).FirstOrDefault();
        }

        public async Task<Expense> GetLeastExpensiveExpenseAsync()
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            return expenses.OrderBy(e => e.Amount).FirstOrDefault();
        }

        public async Task<decimal> GetAverageDailyExpensesAsync(DateTime fromDate, DateTime toDate)
        {
            var expenses = (await _expenseRepository.GetAllExpensesAsync())
                            .Where(e => e.Date >= fromDate && e.Date <= toDate);

            int days = (toDate - fromDate).Days;
            if (days == 0) days = 1;

            return expenses.Any() ? expenses.Sum(e => e.Amount) / days : 0;
        }

        public async Task<decimal> GetAverageMonthlyExpensesAsync(DateTime fromDate, DateTime toDate)
        {
            var expenses = (await _expenseRepository.GetAllExpensesAsync())
                            .Where(e => e.Date >= fromDate && e.Date <= toDate);

            int months = (toDate.Year - fromDate.Year) * 12 + (toDate.Month - fromDate.Month) + 1;
            if (months == 0) months = 1; 

            return expenses.Any() ? expenses.Sum(e => e.Amount) / months : 0;
        }

        public async Task<decimal> GetAverageYearlyExpensesAsync(DateTime fromDate, DateTime toDate)
        {
            var expenses = (await _expenseRepository.GetAllExpensesAsync())
                            .Where(e => e.Date >= fromDate && e.Date <= toDate);

            int years = toDate.Year - fromDate.Year + 1;
            if (years == 0) years = 1; 

            return expenses.Any() ? expenses.Sum(e => e.Amount) / years : 0;
        }

        public async Task<decimal> GetTotalExpensesAsync()
        {
            return (await _expenseRepository.GetAllExpensesAsync()).Sum(e => e.Amount);
        }
    }
}
