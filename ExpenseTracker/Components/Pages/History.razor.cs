using DataModel.Model;
using ExpenseTracker.Services;


namespace ExpenseTracker.Components.Pages
{
    public partial class History
    {
        private string SelectedTag { get; set; } = "All"; // Default selection is "All"
        private List<string> AvailableTags { get; set; } = new List<string>(); // Example tags
        private List<Transaction> FilteredTransactions { get; set; }
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();
        private DateTime? SelectedDate { get; set; } // Nullable to allow no date selection
        private string SelectedType { get; set; } = "All"; // Default selection is "All" (for Debit or Credit)
        protected override void OnInitialized()
        {
            // Load all transactions from the service
            Transactions = TransactionService.GetTransactionHistory();
            FilteredTransactions = Transactions; // Initially, no filter is applied

            AvailableTags = Transactions
            .Select(t => t.Tag)
            .Distinct()
            .OrderBy(tag => tag) // Optional: sort tags alphabetically
            .ToList();
        }

        private void GoBackToDashboard()
        {
            Nav.NavigateTo("/Dashboard");
        }

        // Filter transactions by selected tag
        private void FilterTransactions()
        {
            // Apply tag filter
            var filteredByTag = SelectedTag == "All"
                ? Transactions
                : Transactions.Where(t => t.Tag == SelectedTag).ToList();

            // Apply date filter
            var filteredByDate = SelectedDate.HasValue
                ? filteredByTag.Where(t => t.TransactionDate.Date == SelectedDate.Value.Date).ToList()
                : filteredByTag;

            // Apply transaction type filter
            var filteredByType = SelectedType == "All"
                ? filteredByDate
                : filteredByDate.Where(t => t.Type == SelectedType).ToList();

            // Set the filtered transactions based on both filters independently
            FilteredTransactions = filteredByDate;
            // Set the filtered transactions
            FilteredTransactions = filteredByType;
            // Trigger UI re-render after filter update
            StateHasChanged();
        }
    }
}