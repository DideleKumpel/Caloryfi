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
        //private readonly AuthentitacionService _authentitacionService;

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
                //var result = await _authentitacionService.GetAuthorizationAsync(EmailInput, PasswordInput);
                //if (!result.success)
                //{
                //    ErrorMessage = result.message;
                //    PasswordInput = "";
                //    LoadingIsVisible = false;
                //    return;
                //}
                Application.Current.MainPage = _serviceProvider.GetRequiredService<AppShell>();
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

        public LoginViewModel(/*AuthentitacionService authservice,*/ IServiceProvider serviceProvider)
        {
            ErrorMessage = "";
            //_authentitacionService = authservice;
            _serviceProvider = serviceProvider;
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
                //bool succes = await _authentitacionService.RefreshToken(token);
                //if (succes)
                //{
                //    Application.Current.MainPage = _serviceProvider.GetRequiredService<AppShell>();
                //}
            }
            catch { }
            LoadingIsVisible = false;
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
