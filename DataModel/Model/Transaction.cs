namespace DataModel.Model
{
    public class Transaction
    {
        public string Type { get; set; } // Credit, Debit, Debt
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public string Tag { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? DueDate { get; set; } // Only for Debt
        public string DebtSource { get; set; } // Only for Debt
    }
}
