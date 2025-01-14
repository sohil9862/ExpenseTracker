using DataModel.Model; // Ensure this points to the correct namespace of your Transaction class
using System.Text.Json;

namespace DataModel.Abstraction
{
    public abstract class TransactionBase
    {
        // Define the path to the file where transactions will be saved
        protected static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Transaction.json"
        );

        // Method to load existing transactions from the JSON file
        protected List<Transaction> LoadTransactions()
        {
            if (!File.Exists(FilePath)) return new List<Transaction>();

            var json = File.ReadAllText(FilePath);

            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }

        // Method to save the list of transactions to the JSON file
        protected void SaveTransactions(List<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FilePath, json);
        }
    }
}
