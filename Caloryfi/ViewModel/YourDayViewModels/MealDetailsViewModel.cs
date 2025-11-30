using Caloryfi.Model;
using Caloryfi.View.YourDayViews;
using Caloryfi.Views.DialogPopups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private async Task ChangeFoodDetails(FoodModel selectedFood)
        {
            var PopupResoult = await Application.Current.MainPage.ShowPopupAsync(new InputPopup("Enter weight in grams.", Keyboard.Numeric));
            if (PopupResoult is bool boolResoult)
            {
                if (boolResoult == false)
                {
                    return;
                }
            }
            else if (PopupResoult is string weightString)
            {
                if (double.TryParse(weightString, out double newWeight))
                {
                    selectedFood.Weight = newWeight;
                    // Update other nutritional values based on the new weight if necessary
                }
            }
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
