using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repositories;
using ExpenseTrackerAPI.Services.Dtos;
using AutoMapper;

namespace ExpenseTrackerAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public async Task<ExpenseDTO> GetExpenseByIdAsync(int id)
        {
            var expense = await _expenseRepository.GetExpenseByIdAsync(id);
            return _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task CreateExpenseAsync(ExpenseDTO expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);

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

        public async Task UpdateExpenseAsync(ExpenseDTO expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);

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

        public async Task<ExpenseDTO> GetMostExpensiveExpenseAsync()
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            var mostExpensiveExpense = expenses.OrderByDescending(e => e.Amount).FirstOrDefault();
            return _mapper.Map<ExpenseDTO>(mostExpensiveExpense);
        }

        public async Task<ExpenseDTO> GetLeastExpensiveExpenseAsync()
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();
            var leastExpensiveExpense = expenses.OrderBy(e => e.Amount).FirstOrDefault();
            return _mapper.Map<ExpenseDTO>(leastExpensiveExpense);
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

        public async Task<UserDto> GetUserWithHighestTotalExpensesAsync()
        {
            var expenses = await _expenseRepository.GetAllExpensesAsync();

           
            var userExpenses = expenses
                .GroupBy(e => e.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    TotalAmount = g.Sum(e => e.Amount)
                })
                .OrderByDescending(g => g.TotalAmount)
                .FirstOrDefault();

            if (userExpenses == null)
                return null;

          
            var user = await _userRepository.GetUserByIdAsync(userExpenses.UserId);
            return _mapper.Map<UserDto>(user);
        }

    }
}
