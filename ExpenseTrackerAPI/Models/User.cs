namespace ExpenseTrackerAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal OverAllBudget { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}
