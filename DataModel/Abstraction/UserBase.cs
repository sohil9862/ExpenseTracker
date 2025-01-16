using DataModel.Model;
using System.Text.Json;

namespace DataModel.Abstraction
{
    public abstract class UserBase
    {
        // Path to store user data in the application folder
        protected static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "users.json"
        );

        /// <summary>
        /// Loads the list of users from the JSON file.
        /// </summary>
        /// <returns>List of users if file exists; otherwise, an empty list.</returns>
        protected List<User> LoadUsers()
        {
            if (!File.Exists(FilePath)) return new List<User>();

            var json = File.ReadAllText(FilePath);

            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        /// <summary>
        /// Saves the list of users to the JSON file.
        /// </summary>
        /// <param name="users">List of users to be saved.</param>
        protected void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
