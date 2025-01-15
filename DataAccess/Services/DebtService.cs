using DataModel.Model;
using System.Text.Json;

namespace DataAccess.Services
{
    public class DebtService
    {
        // Path to the file where pending debts will be saved
        public static readonly string PendingDebtsFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PendingDebts.json"
        );

        // Save Debt transaction to PendingDebts.json
        public async Task SaveDebtAsync(Debt debt)
        {
            // Load existing pending debts
            List<Debt> pendingDebts = LoadDebts(PendingDebtsFilePath);
            pendingDebts.Add(debt); // Add the new debt transaction

            // Serialize the pending debts to JSON
            string json = JsonSerializer.Serialize(pendingDebts, new JsonSerializerOptions { WriteIndented = true });

            // Save the data asynchronously
            await File.WriteAllTextAsync(PendingDebtsFilePath, json);
        }

        // Load debts from a specified file
        private List<Debt> LoadDebts(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Debt>(); // Return an empty list if no file exists
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Debt>>(json) ?? new List<Debt>();
        }

        // Get all Pending Debt transactions
        public List<Debt> GetPendingDebts()
        {
            return LoadDebts(PendingDebtsFilePath);
        }
    }
}
