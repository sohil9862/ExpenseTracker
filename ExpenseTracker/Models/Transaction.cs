namespace ExpenseTracker.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } // Credit, Debit, or Debt
        public string Notes { get; set; }
        public string Tags { get; set; }
    }
}

