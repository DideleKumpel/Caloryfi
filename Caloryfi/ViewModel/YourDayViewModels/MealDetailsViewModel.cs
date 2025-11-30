using Caloryfi.Model;
using Caloryfi.Model.DTO;
using Caloryfi.Service;
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
        private readonly MealComponentService _mealComponentService;

        [ObservableProperty]
        private MealModel _meal;

        public MealDetailsViewModel(MealComponentService mealComponentService)
        {
            _mealComponentService = mealComponentService;
        }

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
                    MealComponentDTO UpdatedMeal = new MealComponentDTO
                    {
                        MealId = _meal.Id,
                        IngredientId = selectedFood.Id,
                        Weight = newWeight
                    };
                    var resoult = await _mealComponentService.UpdateMealComponentWeightAsync(UpdatedMeal);
                    if (!resoult.success)
                    {
                        var ErrorPopup = await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(resoult.message));
                    }
                    else
                    {
                        selectedFood.Weight = newWeight;
                    }
                }
            }
        }

        [RelayCommand]
        private async void DeleteFood(FoodModel selectedFood)
        {
            var PopupResoult = await Application.Current.MainPage.ShowPopupAsync(new ConformationMessagePopup("Do you want to delete this component?."));
            if (PopupResoult is bool boolResoult)
            {
                if (boolResoult == false)
                {
                    return;
                }
                else
                {
                    var resoult = await _mealComponentService.DeleteMealComponentAsync(_meal.Id, selectedFood.Id);
                    if (true)
                    {
                        _meal.Ingredients.Remove(selectedFood);
                        //OnPropertyChanged(nameof(Meal));
                    }
                    else
                    {
                        var ErrorPopup = await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(resoult.message));
                    }
                }
            }
        }

        [RelayCommand]
        private void SwitchToAddFoodPage()
        {
            Shell.Current.GoToAsync(nameof(AddFoodView));
        }
    }
}
