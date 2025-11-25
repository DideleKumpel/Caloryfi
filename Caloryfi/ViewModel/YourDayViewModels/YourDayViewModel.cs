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

namespace Caloryfi.ViewModel.YourDayViewModels
{
    public partial class YourDayViewModel : ObservableObject
    {
        private readonly UserSettingsService _userSettingsService;
        private readonly MealService _mealService;

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
        public int MaxProteinForToday { get
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


        public YourDayViewModel(UserSettingsService userSettingsService, MealService mealService)
        {
            _userSettingsService = userSettingsService;
            _mealService = mealService;
            UserSettings = userSettingsService.UserSettings;
            //_mealsList = new ObservableCollection<MealModel> {
            //        new MealModel { Ingredients = new ObservableCollection<FoodModel> { new FoodModel { Weight = 100.0, Kcal=200, Carbs = 10, Fats = 20, Proteins = 20, Name = "food1" },
            //            new FoodModel { Weight = 200.0, Kcal=200, Carbs = 10, Fats = 20, Proteins = 20, Name = "food2" },
            //            new FoodModel { Weight = 100.0, Kcal = 200, Carbs = 10, Fats = 20, Proteins = 20, Name = "food2" }
            //        }
            //    }
            //};
            LoadToDayMeals();
            CalculateTotals();
        }

        [RelayCommand]
        private async Task SwitchToMealDetails(MealModel selectedMeal)
        {
            await Shell.Current.GoToAsync(nameof(View.YourDayViews.MealDetailsView), new Dictionary<string, object>
            {
                { "CurrentMealModel", selectedMeal }
            });
        }

        private async void LoadToDayMeals()
        {
            LoadingIsVisible = true;
            var result =  await _mealService.GetToDaysMeals();
            if (result.success)
            {
                MealsList = JsonConvert.DeserializeObject<ObservableCollection<MealModel>>(result.message);
            }
            else {
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
    }

}
