using DataModel.Model;
using System.Text.Json;

namespace ExpenseTracker.Services
{
    public class TransactionService
    {
        public static readonly string TransactionsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Transactions.json"
        );

        public static readonly string BalanceFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Balance.json"
        );

        private decimal _currentBalance;

        // Constructor: Load the balance when the service is instantiated
        public TransactionService()
        {
            _currentBalance = LoadBalance();
        }

        // Property to get the current balance
        public decimal CurrentBalance => _currentBalance;

        // Save Credit/Debit transaction to Transactions.json
        public async Task SaveTransactionAsync(Transaction transaction)
        {

            if (string.IsNullOrWhiteSpace(transaction.Tag) || transaction.Tag == "Select")
            {
                throw new InvalidOperationException("Please select a valid tag.");
            }


            if (transaction.Type == "Debit" && transaction.Amount > _currentBalance)
            {
                throw new InvalidOperationException("Insufficient balance for this debit transaction.");
            }

            // Update balance based on transaction type
            if (transaction.Type == "Credit")
            {
                _currentBalance += transaction.Amount; // Increase balance for Credit
            }
            else if (transaction.Type == "Debit")
            {
                _currentBalance -= transaction.Amount; // Decrease balance for Debit
            }

            // Save the updated balance
            SaveBalance(_currentBalance);

            // Load existing transactions
            List<Transaction> transactions = LoadTransactions(TransactionsFilePath);
            transactions.Add(transaction);

            // Serialize the transactions to JSON
            string json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

            // Save the data asynchronously
            await File.WriteAllTextAsync(TransactionsFilePath, json);
        }

        // Load transactions from a specified file
        private List<Transaction> LoadTransactions(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Transaction>(); // Return an empty list if no file exists
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

        // Get all Credit/Debit transactions
        public List<Transaction> GetTransactionHistory()
        {
            return LoadTransactions(TransactionsFilePath);
        }

        // Save the current balance to Balance.json
        private void SaveBalance(decimal balance)
        {
            string json = JsonSerializer.Serialize(balance, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(BalanceFilePath, json);
        }

        // Load the balance from Balance.json
        private decimal LoadBalance()
        {
            if (!File.Exists(BalanceFilePath))
            {
                return 10000m; // Default starting balance
            }

            var json = File.ReadAllText(BalanceFilePath);
            return JsonSerializer.Deserialize<decimal>(json);
        }

        // Get the current balance
        public decimal GetCurrentBalance()
        {
            return _currentBalance;
        }
    }
}
