using DataAccess.Services;
using DataModel.Model;

namespace ExpenseTracker.Components.Pages
{
    public partial class PendingDebts
    {
        private List<Debt> PendingDebtsList { get; set; } = new List<Debt>();

        protected override void OnInitialized()
        {
            // Load all pending debts from the service
            PendingDebtsList = DebtService.GetPendingDebts().ToList();
        }

    private void GoBackToDashboard()
        {
            Nav.NavigateTo("/Dashboard");
        }
    }
}