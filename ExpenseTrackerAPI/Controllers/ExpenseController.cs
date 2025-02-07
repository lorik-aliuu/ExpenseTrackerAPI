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

  
    [HttpPost]
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

 
    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpenseById(int id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        return Ok(expense);
    }

  
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllExpenses()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
        return Ok(expenses);
    }

 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(int id, [FromBody] ExpenseDTO expenseDto)
    {
        if (expenseDto == null || expenseDto.Id != id)
        {
            return BadRequest("Expense data is incorrect.");
        }

        await _expenseService.UpdateExpenseAsync(expenseDto);
        return Ok(expenseDto);
    }

   
    [HttpDelete("{id}")]
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
}
