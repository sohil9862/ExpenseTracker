using DataAccess.Interface;
using DataModel.Abstraction;
using DataModel.Model;

namespace DataAccess.Services
{
    public class UserService : UserBase,IUserInterface
    {
        private List<User> _user;

        public const string SeedUserName = "Sohil";
        public const string SeedPassword = "Sohil";
        public UserService()
        {
            _user = LoadUsers();

            if (!_user.Any()) 
            {
                _user.Add(new User { UserName = SeedUserName, Password = SeedPassword });
                SaveUsers(_user);
            }
        }
        public bool Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || (string.IsNullOrEmpty(user.Password)))
            {
                return false;
            }
            return _user.Any(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}
