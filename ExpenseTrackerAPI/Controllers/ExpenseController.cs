using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // Get All Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetAllExpenses()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();
            return Ok(expenses);
        }

        // Get Expense by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound($"Expense with ID {id} not found.");
            }
            return Ok(expense);
        }

        // Create Expense
        [HttpPost]
        public async Task<ActionResult<Expense>> CreateExpense([FromBody] Expense expense)
        {
            try
            {
                await _expenseService.CreateExpenseAsync(expense);
                return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update Expense
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateExpense(int id, [FromBody] Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest("Expense ID mismatch.");
            }

            try
            {
                await _expenseService.UpdateExpenseAsync(expense);
                return NoContent(); // Success, no content to return
            }
            catch (Exception ex)
            {
                return NotFound($"Expense with ID {id} not found. {ex.Message}");
            }
        }

        // Delete Expense
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(int id)
        {
            try
            {
                await _expenseService.DeleteExpenseAsync(id);
                return NoContent(); // Success, no content to return
            }
            catch (Exception ex)
            {
                return NotFound($"Expense with ID {id} not found. {ex.Message}");
            }
        }

        // Get Most Expensive Expense
        [HttpGet("most-expensive")]
        public async Task<ActionResult<Expense>> GetMostExpensiveExpense()
        {
            var expense = await _expenseService.GetMostExpensiveExpenseAsync();
            if (expense == null)
                return NotFound("No expenses found.");
            return Ok(expense);
        }

        // Get Least Expensive Expense
        [HttpGet("least-expensive")]
        public async Task<ActionResult<Expense>> GetLeastExpensiveExpense()
        {
            var expense = await _expenseService.GetLeastExpensiveExpenseAsync();
            if (expense == null)
                return NotFound("No expenses found.");
            return Ok(expense);
        }

        // Get Average Daily Expenses
        [HttpGet("average-daily-expenses")]
        public async Task<ActionResult<decimal>> GetAverageDailyExpenses([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                var avg = await _expenseService.GetAverageDailyExpensesAsync(fromDate, toDate);
                return Ok(avg);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get Total Expenses
        [HttpGet("total-expenses")]
        public async Task<ActionResult<decimal>> GetTotalExpenses()
        {
            var total = await _expenseService.GetTotalExpensesAsync();
            return Ok(total);
        }
    }
}
