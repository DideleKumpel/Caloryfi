using Caloryfi.Model;
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

        [ObservableProperty]
        private UserSettingsModel _currentUserSettings;

        [ObservableProperty]
        private UserModel _userInfo;
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


        public SettingsViewModel(IServiceProvider Service)
        {
            _service = Service;
        }

        [RelayCommand]
        private async Task SaveSettings()
        {
        }
    }
}
