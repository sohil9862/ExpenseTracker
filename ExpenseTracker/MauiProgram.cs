using Microsoft.Extensions.Logging;
using DataAccess.Interface;
using DataAccess.Services;
using ExpenseTracker.Services;

namespace ExpenseTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Register services for Blazor WebView
            builder.Services.AddMauiBlazorWebView();

            // Debugging settings for development
#if DEBUG
            // Add interface implementation for UserService (used in dependency injection)
            builder.Services.AddScoped<IUserInterface, UserService>();

            // Singleton registration for TransactionService
            builder.Services.AddSingleton<TransactionService>();

            builder.Services.AddSingleton<DebtService>();

            // Enable Blazor WebView Developer Tools (for development)
            builder.Services.AddBlazorWebViewDeveloperTools();

            // Enable logging for debugging
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
