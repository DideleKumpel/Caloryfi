using Caloryfi.Model;
using Caloryfi.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel.ProfileViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly IServiceProvider _service;
        private readonly UserService _userService;
        private readonly UserSettingsService _userSettingsService;

        [ObservableProperty]
        private UserSettingsModel _currentUserSettings;

        [ObservableProperty]
        private string _usernameInput;
        [ObservableProperty]
        private string _emailInput;
        [ObservableProperty]
        private string _oldPasswordInput;
        [ObservableProperty]
        private string _passwordInput;
        [ObservableProperty]
        private string _repeatPasswordInput;
        [ObservableProperty]
        private bool _sexInput;
        [ObservableProperty]
        private int _numberOfMealsInput;
        [ObservableProperty]
        private List<String> _dietGoalsPickerList = new List<string>() { "Lose weight", "Maintain weight", "Gain weight" };
        [ObservableProperty]
        private int _selectedDietGoal;
        [ObservableProperty]
        private List<String> _lvlsOfActivityPickerList = new List<string>() { "Sedentary (No exercise, desk job)", "Lightly (Light exercise 1-3days)", "Moderately (Exercise 3-5 days)", "Active (Hard exercise 6-7 days)", "Extremely active (2x day training)" };
        [ObservableProperty]
        private int _selectedActivityLvl;
        [ObservableProperty]
        private int _kcalInput;
        [ObservableProperty]
        private int _proteinProcentage;
        [ObservableProperty]
        private int _proteinNum;
        [ObservableProperty]
        private int _carbsProcentage;
        [ObservableProperty]
        private int _carbsNum;
        [ObservableProperty]
        private int _fatsProcentage;
        [ObservableProperty]
        private int _fatsNum;
        [ObservableProperty]
        private bool _usernameChangedErrorVisible;
        [ObservableProperty]
        private string _usernameChangeErrorMessage;
        [ObservableProperty]
        private bool _emailChangedErrorVisible;
        [ObservableProperty]
        private string _emailChangeErrorMessage;
        [ObservableProperty]
        private bool _passwordChangedErrorVisible;
        [ObservableProperty]
        private string _passwordChangeErrorMessage;


        public SettingsViewModel(IServiceProvider Service, UserService userService, UserSettingsService userSettingsService)
        {
            _service = Service;
            _userService = userService;
            _userSettingsService = userSettingsService;

            UsernameChangedErrorVisible = false;
            EmailChangedErrorVisible = false;
            PasswordChangedErrorVisible = false;

            UsernameInput = _userService.UserModel.Username;
            EmailInput = _userService.UserModel.Email;
            SexInput = _userSettingsService.UserSettings.Sex;
            NumberOfMealsInput = _userSettingsService.UserSettings.NumberOfMeals;
            SelectedDietGoal = (int)_userSettingsService.UserSettings.DietGoal;
            SelectedActivityLvl = (int)_userSettingsService.UserSettings.ActivityLevel;
            KcalInput = (int)_userSettingsService.UserSettings.Kcal;
            ProteinProcentage = (int)(_userSettingsService.UserSettings.Proteins * 100 );
            ProteinNum = (int)(_userSettingsService.UserSettings.Kcal * _userSettingsService.UserSettings.Proteins / 4);
            CarbsProcentage = (int)(_userSettingsService.UserSettings.Carbs * 100);
            CarbsNum = (int)(_userSettingsService.UserSettings.Kcal * _userSettingsService.UserSettings.Carbs / 4);
            FatsProcentage = (int)(_userSettingsService.UserSettings.Fats * 100);
            FatsNum = (int)(_userSettingsService.UserSettings.Kcal * _userSettingsService.UserSettings.Fats / 9);

        }

        [RelayCommand]
        private async Task ChangePassword()
        {
            if (string.IsNullOrWhiteSpace(OldPasswordInput))
            {
                PasswordChangeErrorMessage = "Wirte your old password";
                PasswordChangedErrorVisible = true;
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordInput) || string.IsNullOrWhiteSpace(RepeatPasswordInput))
            {
                PasswordChangeErrorMessage = "Write your new password";
                PasswordChangedErrorVisible = true;
                return;
            }
            if (PasswordInput != RepeatPasswordInput)
            {
                PasswordChangeErrorMessage = "Passwords do not match";
                PasswordChangedErrorVisible = true;
                return;
            }
            if (PasswordInput == OldPasswordInput)
            {
                PasswordChangeErrorMessage = "New and old password are thesame";
                PasswordChangedErrorVisible = true;
                return;
            }
            var result = await _service.GetRequiredService<UserService>().ChangePasswordAsync(OldPasswordInput, PasswordInput);
            PasswordChangeErrorMessage = result.message;
            PasswordChangedErrorVisible = true;
            return;
        }

        [RelayCommand]
        private async Task ChangeEmail()
        {
            if (string.IsNullOrWhiteSpace(EmailInput))
            {
                EmailChangeErrorMessage = "Write your new email";
                EmailChangedErrorVisible = true;
                return;
            }
            if (EmailInput == _userService.UserModel.Email)
            {
                EmailChangeErrorMessage = "New and old email are thesame";
                EmailChangedErrorVisible = true;
                return;
            }
            var result = await _userService.ChangeEmailAsync(EmailInput);
            EmailChangeErrorMessage = result.masage;
            EmailChangedErrorVisible = true;
            return;
        }

        [RelayCommand]
        private async Task SaveSettings()
        {
            
        }

        [RelayCommand]
        private void SexChange(string ChosenChanged)
        {
            if (ChosenChanged == null)
            {
                return;
            }
            if (ChosenChanged == "1")
            {
                SexInput = true;
            }
            else
            {
                SexInput = false;
            }
        }
    }
}
