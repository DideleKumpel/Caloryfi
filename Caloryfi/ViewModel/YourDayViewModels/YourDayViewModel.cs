using Caloryfi.Model;
using Caloryfi.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        [ObservableProperty]
        private ObservableCollection<MealModel> _mealsList = new ObservableCollection<MealModel>();

        [ObservableProperty]
        private UserSettingsModel _userSettings; // use for dispaly of daily calories goal
        [ObservableProperty]
        private int _totalCaloriesForToday;
        [ObservableProperty]
        private int _totalProteinsForToday;
        [ObservableProperty]
        private int _totalCarbsForToday;
        [ObservableProperty]
        private int _totalFatsForToday;

        public YourDayViewModel(IServiceProvider serviceProvider, UserSettingsService userSettingsService)
        {
            _userSettingsService = userSettingsService;
            UserSettings = userSettingsService.UserSettings;
            _mealsList = new ObservableCollection<MealModel> {
                    new MealModel { Ingriedents = new ObservableCollection<FoodModel> { new FoodModel { Weight = 100.0, Kcal=200, Carbs = 10, Fats = 20, Proteins = 20, Name = "food1" },
                        new FoodModel { Weight = 200.0, Kcal=200, Carbs = 10, Fats = 20, Proteins = 20, Name = "food2" },
                        new FoodModel { Weight = 100.0, Kcal = 200, Carbs = 10, Fats = 20, Proteins = 20, Name = "food2" }
                    }
                }
            };
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

        private void CalculateTotals()
        {
            TotalCaloriesForToday = MealsList.Sum(meal => meal.Calories);
            TotalProteinsForToday = MealsList.Sum(meal => meal.Proteins);
            TotalCarbsForToday = MealsList.Sum(meal => meal.Carbs);
            TotalFatsForToday = MealsList.Sum(meal => meal.Fats);
        }
    }

}
