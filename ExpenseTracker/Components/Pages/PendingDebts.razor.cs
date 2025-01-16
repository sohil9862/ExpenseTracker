using DataAccess.Services;
using DataModel.Model;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace ExpenseTracker.Components.Pages
{
    public partial class PendingDebts
    {
        private List<Debt> PendingDebtsList { get; set; } = new List<Debt>();
        private List<Debt> FilteredDebts { get; set; } = new();
        private DateTime? SelectedDueDate { get; set; } // Nullable to allow no date selection
        private DateTime? SelectedDebtDate { get; set; } // Nullable to allow no date selection
        protected override void OnInitialized()
        {
            // Load all pending debts from the service
            PendingDebtsList = DebtService.GetPendingDebts().ToList();
            FilteredDebts = PendingDebtsList;
        }

        private void FilterDebts()
        {
            // Apply due date filter if selected
            var filteredByDueDate = SelectedDueDate.HasValue
                ? PendingDebtsList.Where(d => d.DueDate.Date == SelectedDueDate.Value.Date).ToList()
                : PendingDebtsList;

            // Apply debt date filter if selected
            var filteredByDebtDate = SelectedDebtDate.HasValue
                ? filteredByDueDate.Where(d => d.DebtDate.Date == SelectedDebtDate.Value.Date).ToList()
                : filteredByDueDate;

            // Set the filtered debts
            FilteredDebts = filteredByDebtDate;

            // Trigger UI re-render after filter update
            StateHasChanged();
        }


        private void GoBackToDashboard()
        {
            Nav.NavigateTo("/Dashboard");
        }
    }
}