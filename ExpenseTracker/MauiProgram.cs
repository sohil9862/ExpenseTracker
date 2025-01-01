using Microsoft.Extensions.Logging;
using ExpenseTracker.Services;
using MudBlazor.Services;

namespace ExpenseTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>() // Set the main app class
                .ConfigureFonts(fonts =>
                {
                    // Add custom fonts
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Register services
            builder.Services.AddSingleton<DatabaseService>(); // Ensure DatabaseService is registered as a singleton
            builder.Services.AddMudServices(); // Register MudBlazor services

            // Add support for Blazor WebView
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            // Enable Blazor developer tools and logging in debug mode
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
