namespace DataModel.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; } // Credit, Debit, Debt
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public string Tag { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}
