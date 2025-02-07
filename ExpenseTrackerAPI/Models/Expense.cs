namespace ExpenseTrackerAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public string Description { get; set; }

        //foreign key
        public int UserId { get; set; }

        //Navigation property
        public User User { get; set; }

        //foreign key
        public int CategoryId { get; set; }

        //navigation property
        public Category Category { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
