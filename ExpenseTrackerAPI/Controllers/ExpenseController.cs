using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.Services.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

  
    [HttpPost("AddExpense")]
    public async Task<IActionResult> CreateExpense([FromBody] ExpenseDTO expenseDto)
    {
        if (expenseDto == null)
        {
            return BadRequest("Expense data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _expenseService.CreateExpenseAsync(expenseDto);

        return CreatedAtAction(nameof(GetExpenseById), new { id = expenseDto.Id }, expenseDto);
    }

 
    [HttpGet("GetExpenseById")]
    public async Task<IActionResult> GetExpenseById(int id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        return Ok(expense);
    }

  
    [HttpGet("GetAllExpenses")]
    public async Task<IActionResult> GetAllExpenses()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
        return Ok(expenses);
    }

 
    [HttpPut("UpdateById")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDTO expenseDto)
    {
        if (expenseDto == null || expenseDto.Id != id)
        {
            return BadRequest("Expense data is incorrect.");
        }

        await _expenseService.UpdateExpenseAsync(expenseDto);
        return Ok(expenseDto);
    }

   
    [HttpDelete("DeleteById")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }

        await _expenseService.DeleteExpenseAsync(id);
        return NoContent();
    }

    [HttpGet("get-most-expensive-expense")]
    public async Task<IActionResult> GetMostExpensiveExpense()
    {
        var expense = await _expenseService.GetMostExpensiveExpenseAsync();
        return Ok(expense);
    }

    [HttpGet("get-least-expensive")]
    public async Task<IActionResult> GetLeastExpensiveExpense()
    {
        var expense = await _expenseService.GetLeastExpensiveExpenseAsync();
        return Ok(expense);
    }

    [HttpGet("get-average-daily")]
    public async Task<IActionResult> GetAverageDailyExpenses([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var avgExpense = await _expenseService.GetAverageDailyExpensesAsync(fromDate, toDate);
        return Ok(avgExpense);
    }

    [HttpGet("get-average-monthly")]
    public async Task<IActionResult> GetAverageMonthlyExpenses([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var avgExpense = await _expenseService.GetAverageMonthlyExpensesAsync(fromDate, toDate);
        return Ok(avgExpense);
    }

    [HttpGet("get-average-yearly")]
    public async Task<IActionResult> GetAverageYearlyExpenses([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
    {
        var avgExpense = await _expenseService.GetAverageYearlyExpensesAsync(fromDate, toDate);
        return Ok(avgExpense);
    }

    [HttpGet("get-total-expenses")]
    public async Task<IActionResult> GetTotalExpenses()
    {
        var totalExpenses = await _expenseService.GetTotalExpensesAsync();
        return Ok(totalExpenses);
    }

    [HttpGet("get-user-with-highest-total-expenses")]
    public async Task<IActionResult> GetUserWithHighestTotalExpenses()
    {
        var user = await _expenseService.GetUserWithHighestTotalExpensesAsync();
        if (user == null)
        {
            return NotFound("No user found with expenses.");
        }
        return Ok(user);
    }
}
