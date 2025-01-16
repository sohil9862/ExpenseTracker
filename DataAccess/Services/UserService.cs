using DataAccess.Interface;
using DataModel.Abstraction;
using DataModel.Model;

namespace DataAccess.Services
{
    public class UserService : UserBase, IUserInterface
    {
        private const string SeedUsername = "admin";
        private const string SeedPassword = "password";

        public UserService()
        {
            // No need to load users, just initialize the seed user.
        }

        /// <summary>
        /// Handles user login logic for the seed user.
        /// </summary>
        /// <param name="user">The user object containing username and password.</param>
        /// <returns>True if login is successful; otherwise, false.</returns>
        public bool Login(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                return false; // Invalid login input
            }

            // Only allow login if the username and password match the seed user
            return user.UserName == SeedUsername && user.Password == SeedPassword;
        }
    }
}
