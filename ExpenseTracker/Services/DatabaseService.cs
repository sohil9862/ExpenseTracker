using SQLite;
using System.IO;
using System.Threading.Tasks;
using ExpenseTracker.Models;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTracker.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _db;

        public async Task InitializeAsync()
        {
            if (_db != null)
                return;

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ExpenseTracker.db");
            _db = new SQLiteAsyncConnection(dbPath);
            await _db.CreateTableAsync<User>();
            await _db.CreateTableAsync<Transaction>();
            await _db.CreateTableAsync<Debt>();
        }

        // Add a new user to the database
        public Task<int> AddUserAsync(User user) => _db.InsertAsync(user);

        // Get a user by username and verify password
        public async Task<User> GetUserAsync(string username, string password)
        {
            var user = await _db.Table<User>().FirstOrDefaultAsync(u => u.Username == username);
            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }
            return null;
        }

        // Verify the hashed password
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            return storedPasswordHash == HashPassword(inputPassword);
        }

        // Hash the password using SHA256
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // Add a transaction to the database
        public Task<int> AddTransactionAsync(Transaction transaction) => _db.InsertAsync(transaction);

        // Add a debt to the database
        public Task<int> AddDebtAsync(Debt debt) => _db.InsertAsync(debt);

        // Get transactions for a user
        public Task<List<Transaction>> GetTransactionsAsync(int userId) =>
            _db.Table<Transaction>().Where(t => t.Id == userId).ToListAsync();

        // Get debts for a user
        public Task<List<Debt>> GetDebtsAsync(int userId) =>
            _db.Table<Debt>().Where(d => d.Id == userId && !d.IsCleared).ToListAsync();
    }
}
