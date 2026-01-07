using Caloryfi.Model;
using Caloryfi.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel.HistoryViewModels
{
    public partial class HistoryViewModel : ObservableObject
    {
        private readonly UserSettingsService _userSettingsService;
        private readonly MealService _mealService;

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Now.AddDays(-1);
        [ObservableProperty]
        private DateTime _maximumDate = DateTime.Now;

        [ObservableProperty]
        private ObservableCollection<MealModel> _mealsList = new ObservableCollection<MealModel>();

        [ObservableProperty]
        private UserSettingsModel _userSettings; // use for dispaly of daily calories goal
        [ObservableProperty]
        private bool _ErrorMessageVisible;
        [ObservableProperty]
        private bool _loadingIsVisible;
        [ObservableProperty]
        private int _totalCaloriesForToday;
        [ObservableProperty]
        private int _totalProteinsForToday;
        [ObservableProperty]
        private int _totalCarbsForToday;
        [ObservableProperty]
        private int _totalFatsForToday;
        public int MaxProteinForToday
        {
            get
            {
                return (int)(_userSettings.Kcal * _userSettings.Proteins / 4);
            }
        }
        public int MaxCarbsForToday
        {
            get
            {
                return (int)(_userSettings.Kcal * _userSettings.Carbs / 4);
            }
        }
        public int MaxFatsnForToday
        {
            get
            {
                return (int)(_userSettings.Kcal * _userSettings.Fats / 9);
            }
        }

        public HistoryViewModel(UserSettingsService userSettingsService, MealService mealService)
        {
            _userSettingsService = userSettingsService;
            _mealService = mealService;
            UserSettings = _userSettingsService.UserSettings;
            LoadMealsForSelectedDate();
        }


        [RelayCommand]
        private async Task NextDate()
        {
            if (SelectedDate < MaximumDate)
            {
                SelectedDate = SelectedDate.AddDays(1);
                await LoadMealsForSelectedDate();
            }
        }

        partial void OnSelectedDateChanged(DateTime oldValue, DateTime newValue)
        {
            LoadMealsForSelectedDate();
        }

        [RelayCommand]
        private async Task PreviousDate()
        {
            SelectedDate = SelectedDate.AddDays(-1);
            await LoadMealsForSelectedDate();
        }

        private async Task LoadMealsForSelectedDate()
        {
            LoadingIsVisible = true;
            var result = await _mealService.GetMealByDate(SelectedDate);
            if (result.success)
            {
                MealsList = JsonConvert.DeserializeObject<ObservableCollection<MealModel>>(result.message);
            }
            else
            {
                ErrorMessageVisible = true;
            }
            CalculateTotals();
            LoadingIsVisible = false;
        }

        private void CalculateTotals()
        {
            TotalCaloriesForToday = MealsList.Sum(meal => meal.Calories);
            TotalProteinsForToday = MealsList.Sum(meal => meal.Proteins);
            TotalCarbsForToday = MealsList.Sum(meal => meal.Carbs);
            TotalFatsForToday = MealsList.Sum(meal => meal.Fats);
        }

        [RelayCommand]
        private async Task SwitchToHsitoryMealDetails(MealModel selcetedMeal)
        {
            await Shell.Current.GoToAsync(nameof(View.HistoryViews.HistoryMealDetailView), new Dictionary<string, object>
            {
                { "CurrentMealModel", selcetedMeal}
            });
        }
    }
}
