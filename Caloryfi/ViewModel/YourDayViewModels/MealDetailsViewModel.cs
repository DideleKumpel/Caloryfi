using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caloryfi.Model;
using Caloryfi.View.YourDayViews;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Caloryfi.ViewModel.YourDayViewModels
{
    public partial class MealDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private MealModel _meal;

        [RelayCommand]
        private void AddFood()
        {
            // Logic to add food to the meal
        }

        [RelayCommand]
        private void ShowFoodDetails(FoodModel selectedFood)
        {
            // Logic to show food details
        }

        [RelayCommand]
        private void DeleteFood(FoodModel selectedFood)
        {
            // Logic to delete food from the meal
        }

        [RelayCommand]
        private void SwitchToAddFoodPage()
        {
            Shell.Current.GoToAsync(nameof(AddFoodView));
        }
    }
}
