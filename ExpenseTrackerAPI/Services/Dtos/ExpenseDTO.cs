namespace ExpenseTrackerAPI.Services.Dtos
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
