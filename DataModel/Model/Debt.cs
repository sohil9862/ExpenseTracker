namespace DataModel.Model
{
    public class Debt
    {
        public int DebtId { get; set; }
        // Amount of the debt
        public decimal DebtAmount { get; set; }

        // Due date for the debt
        public DateTime DueDate { get; set; } = DateTime.Now;

        public string DebtTag { get; set; }

        // Source of the debt (e.g., lender name, institution)
        public string DebtSource { get; set; }

        // Notes or description about the debt
        public string DebtNotes { get; set; }

        // Date when the debt was created
        public DateTime DebtDate { get; set; } = DateTime.Now;
    }
}
