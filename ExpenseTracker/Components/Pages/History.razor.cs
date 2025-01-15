using DataModel.Model;
using ExpenseTracker.Services;

namespace ExpenseTracker.Components.Pages
{
    public partial class History
    {
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();

        protected override void OnInitialized()
        {
            // Load all transactions from the service
            Transactions = TransactionService.GetTransactionHistory();
        }

        private void GoBackToDashboard()
        {
            Nav.NavigateTo("/Dashboard");
        }
    }
}