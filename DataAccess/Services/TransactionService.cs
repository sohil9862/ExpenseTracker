using DataModel.Model;
using System.Text.Json;

namespace ExpenseTracker.Services
{
    public class TransactionService
    {
        public static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Transactions.json"
        );

        private decimal _currentBalance = 0m; // Starting balance at 0

        // Property to get current balance
        public decimal CurrentBalance => _currentBalance;

        // Save transaction method (updated for balance check)
        public async Task SaveTransactionAsync(Transaction transaction)
        {
            if (transaction.Type == "Debit" && transaction.Amount > _currentBalance)
            {
                throw new InvalidOperationException("Insufficient balance for this debit transaction.");
            }

            // Update balance based on transaction type
            if (transaction.Type == "Credit")
            {
                _currentBalance += transaction.Amount; // Increase balance for Credit transactions
            }
            else if (transaction.Type == "Debit")
            {
                _currentBalance -= transaction.Amount; // Decrease balance for Debit transactions
            }

            // Load existing transactions
            List<Transaction> transactions = LoadTransactions();
            transactions.Add(transaction); // Add the new transaction

            // Serialize the transactions to JSON
            string json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

            // Save the data asynchronously
            await File.WriteAllTextAsync(FilePath, json);
        }

        // Load all transactions from the file
        public List<Transaction> LoadTransactions()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Transaction>(); // Return an empty list if no file exists
            }

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

        // Get all transactions
        public List<Transaction> GetTransactions()
        {
            return LoadTransactions();
        }

        // Method to clear transactions
        public async Task ClearTransactionsAsync()
        {
            if (File.Exists(FilePath))
            {
                // Delete the file to clear the history
                File.Delete(FilePath);
            }

            // Optionally reset balance here if needed
            _currentBalance = 0m; ;
        }

        public decimal GetCurrentBalance()
        {
            return _currentBalance;
        }
    }
}
