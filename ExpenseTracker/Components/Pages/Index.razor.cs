namespace ExpenseTracker.Components.Pages
{
    public partial class Index
    {
        protected override void OnInitialized()
        {
            Nav.NavigateTo("/login");
        }
    }
}