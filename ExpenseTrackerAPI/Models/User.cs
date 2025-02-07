﻿namespace ExpenseTrackerAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Budget { get; set; }

        public List<Expense> Expenses { get; set; }
    }
}
