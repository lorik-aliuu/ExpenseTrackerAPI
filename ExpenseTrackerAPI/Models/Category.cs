using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Models;

public class Category
{

    public int Id { get; set; }
    public string Name { get; set; }

    public decimal Budget { get; set; }

    public ICollection<Expense> Expenses { get; set; }

}
