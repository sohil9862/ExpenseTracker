using DataModel.Model; // Ensure the namespace points to the Debt class
using System.Text.Json;

namespace DataModel.Abstraction
{
    public abstract class DebtBase
    {
        // Define the path for the Pending Debts file
        protected static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PendingDebts.json"
        );

        // Method to load debts from the JSON file
        protected List<Debt> LoadDebts()
        {
            if (!File.Exists(FilePath)) return new List<Debt>();

            var json = File.ReadAllText(FilePath);

            return JsonSerializer.Deserialize<List<Debt>>(json) ?? new List<Debt>();
        }

        // Method to save the list of debts to the JSON file
        protected void SaveDebts(List<Debt> debts)
        {
            var json = JsonSerializer.Serialize(debts, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(FilePath, json);
        }
    }
}
