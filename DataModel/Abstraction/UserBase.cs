using DataModel.Model;
using System.Text.Json;


namespace DataModel.Abstraction
{
    public abstract class UserBase
    {
        protected static readonly string FilePath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
    "Sohil.json"
);

        protected List<User> LoadUsers()
        {
            if (!File.Exists(FilePath)) return new List<User>();

            var json = File.ReadAllText(FilePath);

            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        protected void SaveUsers(List<User> users)
        {

            var json = JsonSerializer.Serialize(users);

            File.WriteAllText(FilePath, json);
        }
    }
}
