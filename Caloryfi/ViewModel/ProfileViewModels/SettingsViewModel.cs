using Caloryfi.Model;
using Caloryfi.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private readonly WeightHistoryService _weightHistoryService;

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
        private uint _kcalInput;
        [ObservableProperty]
        private  uint _proteinProcentage;
        [ObservableProperty]
        private int _proteinNum;
        [ObservableProperty]
        private uint _carbsProcentage;
        [ObservableProperty]
        private int _carbsNum;
        [ObservableProperty]
        private uint _fatsProcentage;
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
        [ObservableProperty]
        private bool _settingsChangedErrorVisible;
        [ObservableProperty]
        private string _settingsChangeErrorMessage;


        public SettingsViewModel(IServiceProvider Service, UserService userService, UserSettingsService userSettingsService, WeightHistoryService weightHistoryService)
        {
            _service = Service;
            _userService = userService;
            _userSettingsService = userSettingsService;
            _weightHistoryService = weightHistoryService;

            UsernameChangedErrorVisible = false;
            EmailChangedErrorVisible = false;
            PasswordChangedErrorVisible = false;

            UsernameInput = _userService.UserModel.Username;
            EmailInput = _userService.UserModel.Email;
            SexInput = _userSettingsService.UserSettings.Sex;
            NumberOfMealsInput = _userSettingsService.UserSettings.NumberOfMeals;
            SelectedDietGoal = (int)_userSettingsService.UserSettings.DietGoal;
            SelectedActivityLvl = (int)_userSettingsService.UserSettings.ActivityLevel;
            KcalInput = (uint)_userSettingsService.UserSettings.Kcal;
            ProteinProcentage = (uint)(_userSettingsService.UserSettings.Proteins * 100 );
            ProteinNum = (int)(_userSettingsService.UserSettings.Kcal * _userSettingsService.UserSettings.Proteins / 4);
            CarbsProcentage = (uint)(_userSettingsService.UserSettings.Carbs * 100);
            CarbsNum = (int)(_userSettingsService.UserSettings.Kcal * _userSettingsService.UserSettings.Carbs / 4);
            FatsProcentage = (uint)(_userSettingsService.UserSettings.Fats * 100);
            FatsNum = (int)(_userSettingsService.UserSettings.Kcal * _userSettingsService.UserSettings.Fats / 9);

        }

        [RelayCommand]
        private async Task ChangePassword()
        {
            if (string.IsNullOrWhiteSpace(OldPasswordInput))
            {
                PasswordChangeErrorMessage = "Wirte your old password";
            }
            else if (string.IsNullOrWhiteSpace(PasswordInput) || string.IsNullOrWhiteSpace(RepeatPasswordInput))
            {
                PasswordChangeErrorMessage = "Write your new password";
            }
            else if (PasswordInput != RepeatPasswordInput)
            {
                PasswordChangeErrorMessage = "Passwords do not match";
            }
            else if (PasswordInput == OldPasswordInput)
            {
                PasswordChangeErrorMessage = "New and old password are thesame";
            }
            else
            {
                var result = await _service.GetRequiredService<UserService>().ChangePasswordAsync(OldPasswordInput, PasswordInput);
                PasswordChangeErrorMessage = result.message;
            }
            PasswordChangedErrorVisible = true;
            return;
        }

        [RelayCommand]
        private async Task ChangeEmail()
        {
            if (string.IsNullOrWhiteSpace(EmailInput))
            {
                EmailChangeErrorMessage = "Write your new email";
            }
            else if (EmailInput == _userService.UserModel.Email)
            {
                EmailChangeErrorMessage = "New and old email are thesame";
            }
            else
            {
                var result = await _userService.ChangeEmailAsync(EmailInput);
                EmailChangeErrorMessage = result.masage;
            }
            EmailChangedErrorVisible = true;
            return;
        }

        [RelayCommand]
        private async Task ChangeUsername()
        {
            if (string.IsNullOrWhiteSpace(UsernameInput))
            {
                UsernameChangeErrorMessage = "Write new username";
                
            }
            else if (UsernameInput == _userService.UserModel.Username) 
            {
                UsernameChangeErrorMessage = "New username is same as old one";
            }
            else
            {
                var result = await _userService.ChangeUsername(UsernameInput);
                UsernameChangeErrorMessage = result.masage;
            }
            UsernameChangedErrorVisible = true;
            return;
        }

        [RelayCommand]
        private async Task SaveSettings()
        {
            // chcek if macros add up to 100%
            if (ProteinProcentage + CarbsProcentage + FatsProcentage != 100)
            {
                SettingsChangeErrorMessage = "Macros must add up to 100%";
                SettingsChangedErrorVisible = true;
                return;
            }
            try
            {
                var updatedSettings = new UserSettingsModel
                {
                    Id = _userSettingsService.UserSettings.Id,
                    UserId = _userSettingsService.UserSettings.UserId,
                    Sex = SexInput,
                    NumberOfMeals = NumberOfMealsInput,
                    DietGoal = SelectedDietGoal,
                    ActivityLevel = SelectedActivityLvl,
                    Kcal = KcalInput,
                    Carbs = (Decimal)CarbsProcentage / 100,
                    Proteins = (Decimal)ProteinProcentage / 100,
                    Fats = (Decimal)FatsProcentage / 100
                };
                var result = await _userSettingsService.UpdateUserSettingsAsync(updatedSettings);
                SettingsChangeErrorMessage = result.message;
                SettingsChangedErrorVisible = true;
                return;
            }
            catch
            {
                SettingsChangeErrorMessage = "An error occurred while saving settings.";
                SettingsChangedErrorVisible = true;
                return;
            }
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

        [RelayCommand]
        private void AutoCalculateMacros()
        {
            double CaloricDemand = _weightHistoryService.CurrentWeight.Weight * 10;
            if (SexInput) // for female
            {
                CaloricDemand += 700;
            }
            else //for male
            {
                CaloricDemand += 900;
            }
            switch (SelectedActivityLvl)
            {
                case 0:
                    CaloricDemand *= 1.2;
                    break;
                case 1:
                    CaloricDemand *= 1.35;
                    break;
                case 2:
                    CaloricDemand *= 1.5;
                    break;
                case 3:
                    CaloricDemand *= 1.65;
                    break;
                case 4:
                    CaloricDemand *= 1.8;
                    break;
            }

            KcalInput =  (uint)CaloricDemand;

            ProteinProcentage = 20;
            ProteinNum = (int)(KcalInput * 0.2 / 4);
            CarbsProcentage = 50;
            CarbsNum = (int)(KcalInput * 0.5 / 4);
            FatsProcentage = 30;
            FatsNum = (int)(KcalInput * 0.3 / 9);
            
        }

        [RelayCommand]
        private void RecalculateMakroInGrams()
        {
            ProteinNum = (int)((KcalInput * ((double)ProteinProcentage / 100)) / 4);
            CarbsNum = (int)((KcalInput * ((double)CarbsProcentage / 100)) / 4);
            FatsNum = (int)((KcalInput * ((double)FatsProcentage / 100)) / 9);
        }
    }
}
