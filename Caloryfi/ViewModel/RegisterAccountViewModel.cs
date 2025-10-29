using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Caloryfi.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel
{
    public partial class RegisterAccountViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        //private readonly UserService _userService;

        [ObservableProperty]
        public string _nicknameInput;
        [ObservableProperty]
        public string _emailInput;
        [ObservableProperty]
        public string _passwordInput;
        [ObservableProperty]
        public string _passwordRepeatInput;
        [ObservableProperty]
        public string _errorMessage;
        [ObservableProperty]
        public string _succesMessage;
        [ObservableProperty]
        public bool _loadingIsVisible;

        [RelayCommand]
        public void SwitchToLoginPanel()
        {
            Application.Current.MainPage = _serviceProvider.GetRequiredService<LoginView>();
        }

        [RelayCommand]
        public async Task RegisterAccount()
        {
            LoadingIsVisible = true;
            SuccesMessage = "";
            ErrorMessage = "";
            if (PasswordInput != PasswordRepeatInput)
            {
                ErrorMessage = "Password are not the same.";
                LoadingIsVisible = false;
                return;
            }
            //Rest validation is API site
            //var response = await _userService.CreateAccountAsync(NicknameInput, EmailInput, PasswordInput);
            //if (response.success)
            //{
            //    ErrorMessage = response.message;
            //    SuccesMessage = "Account has been created go to login page";
            //}
            //ErrorMessage = response.message;
            LoadingIsVisible = false;
        }

        public RegisterAccountViewModel(IServiceProvider serviceProvider /*, UserService userService*/)
        {
            _loadingIsVisible = false;
            ErrorMessage = "";
            _serviceProvider = serviceProvider;
            //_userService = userService;
        }
    }
}
