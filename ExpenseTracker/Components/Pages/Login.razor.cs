using DataModel.Model;

namespace Money.Components.Pages
{
    public partial class Login
    {
        private User Users { get; set; } = new User();

        private string ErrorMessage { get; set; } = string.Empty;

        private void HandleLogin() 
        {
            if (UserInterface.Login(Users)) 
            {
                Nav.NavigateTo("/Dashboard");
            }
            else
            {
                ErrorMessage = "UserName or Password is invalid";
            }
        }
    }
}