using Caloryfi.Service;
using Caloryfi.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel
{
    public partial class LoginViewModel: ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserService _userService;
        private readonly UserSettingsService _userSettingsService;
        private readonly WeightHistoryService _weightHistoryService;

        [ObservableProperty]
        public string _emailInput;
        [ObservableProperty]
        public string _passwordInput;
        [ObservableProperty]
        public string _errorMessage;
        [ObservableProperty]
        public bool _loadingIsVisible;


        [RelayCommand]
        public async Task Login()
        {
            LoadingIsVisible = true;
            ErrorMessage = "";
            if (!IsValidEmail(EmailInput))
            {
                ErrorMessage = "Invalid email format.\n";
                LoadingIsVisible = false;
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordInput))
            {
                ErrorMessage = "Please fill in all fields.\n";
                LoadingIsVisible = false;
                return;
            }
            try
            {
                var result = await _userService.LogInAsync(EmailInput, PasswordInput);
                if (!result.success)
                {
                    ErrorMessage = result.message;
                    PasswordInput = "";
                    LoadingIsVisible = false;
                    return;
                }
                if (await TryDownloadUserData())
                {
                    Application.Current.MainPage = _serviceProvider.GetRequiredService<AppShell>();
                }
                return;
            }
            catch
            {
                ErrorMessage = "Error occured while loggin try again later.";
                LoadingIsVisible = false;
                return;
            }

        }
        [RelayCommand]
        public void SwitchToRegisterPage()
        {
            Application.Current.MainPage = _serviceProvider.GetRequiredService<RegisterAccountView>();
        }

        public LoginViewModel(UserService userService, IServiceProvider serviceProvider, UserSettingsService userSettings, WeightHistoryService weightHistoryService)
        {
            ErrorMessage = "";
            _userService = userService;
            _serviceProvider = serviceProvider;
            _userSettingsService = userSettings;
            _weightHistoryService = weightHistoryService;
            _loadingIsVisible = false;
            TryToLogIn();
        }

        private async void TryToLogIn()
        {
            LoadingIsVisible = true;
            try
            {
                string token = await SecureStorage.GetAsync("AuthTokenKey");
                if (string.IsNullOrEmpty(token))
                {
                    LoadingIsVisible = false;
                    return;
                }
                bool succes = await _userService.RefreshToken(token);
                if (!succes)
                {
                    LoadingIsVisible = false;
                    return;
                }
                if (await TryDownloadUserData())
                {
                    Application.Current.MainPage = _serviceProvider.GetRequiredService<AppShell>(); 
                }
                LoadingIsVisible = false;
                return;
            }
            catch { }
            LoadingIsVisible = false;
        }

        private async Task<bool> TryDownloadUserData()
        {
            var UserDataResult = await _userService.GetUserInfoAsync();
            if (!UserDataResult)
            {
                ErrorMessage = "Can't download userdata";
                LoadingIsVisible = false;
                return false;
            }
            var UserSettingsResult = await _userSettingsService.GetUserSettingsAsync();
            if (!UserSettingsResult.success)
            {
                ErrorMessage = "Can't download usersettings";
                LoadingIsVisible = false;
                return false;
            }
            var WeightHistoryResult = await _weightHistoryService.GetCurrentWeightAsync();
            if (!WeightHistoryResult.success)
            {
                ErrorMessage = "Can't download weight history";
                LoadingIsVisible = false;
                return false;
            }
            return true;
        }
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return System.Text.RegularExpressions.Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}
