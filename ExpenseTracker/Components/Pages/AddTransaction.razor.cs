using DataModel.Model;
using ExpenseTracker.Services;

namespace ExpenseTracker.Components.Pages
{
    public partial class AddTransaction
    {
        private string TransactionType { get; set; } = "Select";
        private string TransactionTags { get; set; } = "Select Tag";
        private Transaction Transaction { get; set; } = new Transaction();
        private Debt Debt { get; set; } = new Debt();
        private string ErrorMessage { get; set; }

        private async Task SaveTransaction()
        {
            try
            {
                if (TransactionType == "Credit" && TransactionType == "Debit")
                {
                    // Validate transaction type
                    if (TransactionType == "Select")
                    {
                        ErrorMessage = "Please select a transaction type.";
                        return;
                    }

                    // Validate tag selection for both types (Credit, Debit, Debt)
                    if (Transaction.Tag == "Select Tag" || string.IsNullOrEmpty(Transaction.Tag))
                    {
                        ErrorMessage = "Please select a valid tag.";
                        return;
                    }
                }

                // Additional validation for debt transactions
                if (TransactionType == "Debt")
                {
                    // Ensure DebtAmount is greater than zero
                    if (Debt.DebtAmount <= 0)
                    {
                        ErrorMessage = "Debt amount must be greater than zero.";
                        return;
                    }

                    if (TransactionType == "Select")
                    {
                        ErrorMessage = "Please select a transaction type.";
                        return;
                    }

                    // Ensure DebtSource is not empty or null
                    if (string.IsNullOrWhiteSpace(Debt.DebtSource))
                    {
                        ErrorMessage = "Please provide a debt source.";
                        return;
                    }

                    // Process the debt transaction
                    await DebtService.SaveDebtAsync(Debt);
                }
                else
                {
                    // Process regular transaction
                    Transaction.Type = TransactionType;
                    // Assign the selected tag to the transaction
                    Transaction.Tag = TransactionTags;

                    // If a valid tag is selected
                    await TransactionService.SaveTransactionAsync(Transaction);
                }

                // Reset forms and navigate back
                Transaction = new Transaction();
                Debt = new Debt();
                TransactionType = "Select";  // Reset transaction type
                TransactionTags = "Select Tag";  // Reset tag selection
                ErrorMessage = string.Empty;

                // Optionally, navigate back to the dashboard
                Nav.NavigateTo("/Dashboard");
            }
            catch (Exception ex)
            {
                // Handle any errors during the saving process
                ErrorMessage = ex.Message;
            }

        }
        private void GoBackToDashboard()
        {
            Nav.NavigateTo("/Dashboard");
        }

    }
}
