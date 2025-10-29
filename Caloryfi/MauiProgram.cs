using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace Caloryfi
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //API CONNECTION SETTINGS
            builder.Services.AddSingleton<HttpClient>(serviceProvider =>
            {
                var apiBaseUrl = "https://5e79fd12faf5.ngrok-free.app";

                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(apiBaseUrl),
                    Timeout = TimeSpan.FromSeconds(30)
                };

                // Konfiguracja nagłówków
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                return httpClient;
            });

            //APPSHELL
            builder.Services.AddSingleton<AppShell>();

            //SERVICE REGISTRATIONS

            //VIEW
            builder.Services.AddTransient<View.LoginView>();
            builder.Services.AddTransient<View.RegisterAccountView>();

            //VIEWMODEL
            builder.Services.AddTransient<ViewModel.LoginViewModel>();
            builder.Services.AddTransient<ViewModel.RegisterAccountViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
