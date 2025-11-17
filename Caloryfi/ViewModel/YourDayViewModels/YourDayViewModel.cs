using Caloryfi.Model;
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
        [ObservableProperty]
        private ObservableCollection<MealModel> _mealsList = new ObservableCollection<MealModel>();

        [ObservableProperty]
        private UserSettingsModel userSettingsModel; // use for dispaly of daily calories goal
        [ObservableProperty]
        private int _totalCaloriesForToday;
        [ObservableProperty]
        private int _totalProteinsForToday;
        [ObservableProperty]
        private int _totalCarbsForToday;
        [ObservableProperty]
        private int _totalFatsForToday;


    }
}
