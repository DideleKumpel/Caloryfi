using Caloryfi.Model;
using Caloryfi.Service;
using CommunityToolkit.Mvvm.ComponentModel;
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
            _mealsList = new ObservableCollection<MealModel> { new MealModel { Calories = 1234, Proteins= 123, Carbs=123, Fats=123 }, 
                new MealModel { Calories = 1234, Proteins = 123, Carbs = 123, Fats = 123 } };
        }
    }
}
