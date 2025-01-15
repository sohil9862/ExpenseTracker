using DataModel.Model;
using ExpenseTracker.Services;
using System.Diagnostics;

namespace ExpenseTracker.Components.Pages
{
    public partial class Dashboard
    {
        private decimal currentBalance { get; set; }
        private decimal CashFlowIn { get; set; }
        private decimal CashFlowOut { get; set; }
        private decimal TotalTransactions { get; set; }
        private int TotalTransactionCount { get; set; }
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();
        private List<Debt> PendingDebts { get; set; } = new List<Debt>();
        private const int MaxTransactionsToShow = 5; // Show the latest 5 transactions
        private const int MaxPendingDebtsToShow = 5; // Show the latest 5 pending debts
        private decimal RemainingDebts { get; set; }
        private decimal ClearedDebts { get; set; }
        protected override void OnInitialized()
        {
            LoadTransactionData();
            LoadPendingDebts();
            LoadDebtData();
            CalculateTotalTransactions();
        }

        private void LoadTransactionData()
        {
            var transactions = TransactionService.GetTransactionHistory();

            // Update the list of transactions to display (latest 5)
            Transactions = transactions.OrderByDescending(t => t.TransactionDate).Take(MaxTransactionsToShow).ToList();

            // Update cash flow in and out
            CashFlowIn = transactions.Where(t => t.Type == "Credit").Sum(t => t.Amount);
            CashFlowOut = transactions.Where(t => t.Type == "Debit").Sum(t => t.Amount);

            // Update total balance
            currentBalance = TransactionService.GetCurrentBalance();
        }

        private void LoadPendingDebts()
        {
            // Get the debts from the debt service
            var debts = DebtService.GetPendingDebts();

            // Filter the debts to show only pending ones (assuming "Pending" status exists)
            PendingDebts = debts.OrderByDescending(d => d.DueDate).Take(MaxPendingDebtsToShow).ToList();
        }

        private void LoadDebtData()
        {
            var debts = DebtService.GetPendingDebts();

            // Calculate the remaining debts (assumed to be 'Pending' status)
            RemainingDebts = debts.Sum(d => d.DebtAmount);

            // Calculate the cleared debts (assumed to be 'Cleared' status)
            ClearedDebts = 0;
        }

        private void GoToAddTransaction()
        {
            Nav.NavigateTo("/AddTransaction");
        }

        private void Logout()
        {
            Nav.NavigateTo("/Login");
        }

        private void GoToHistory()
        {
            Nav.NavigateTo("/History");
        }

        private void NavigateToPendingDebts()
        {
            Nav.NavigateTo("/PendingDebts");
        }

        public List<Debt> GetTopPendingDebts()
        {
            // Assuming `debts` is a collection of Debt objects
            var pendingDebts = PendingDebts
                .OrderByDescending(d => d.DebtAmount) // Sort by amount in descending order
                .Take(MaxPendingDebtsToShow) // Take top 5
                .ToList();

            return pendingDebts;
        }

        private void CalculateTotalTransactions()
        {
            var transactions = TransactionService.GetTransactionHistory();

            // Calculate total transaction count
            TotalTransactionCount = transactions.Count;

            // Calculate total transaction value (inflows + debts - outflows)
            decimal totalInflow = transactions.Where(t => t.Type == "Credit").Sum(t => t.Amount);
            decimal totalOutflow = transactions.Where(t => t.Type == "Debit").Sum(t => t.Amount);
            decimal totalDebt = PendingDebts.Sum(d => d.DebtAmount);

            TotalTransactions = totalInflow + totalDebt - totalOutflow;
        }
    }
}
