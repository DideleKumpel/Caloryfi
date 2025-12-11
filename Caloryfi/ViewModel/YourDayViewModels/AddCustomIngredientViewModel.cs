using Caloryfi.Model;
using Caloryfi.Model.DTO;
using Caloryfi.Service;
using Caloryfi.View;
using Caloryfi.Views.DialogPopups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Caloryfi.ViewModel.YourDayViewModels;

public partial class AddCustomIngredientViewModel : ObservableObject
{
    private readonly IngredientsService _ingredientsService;
    private readonly MealComponentService _mealComponentService;
    private readonly AIService _aIService;

    private AddFoodViewModel _addFoodViewModel;
    public AddFoodViewModel AddFoodViewModel
    {
        set { _addFoodViewModel = value; }
    }

    [ObservableProperty] string _name;
    [ObservableProperty] uint _weight;
    [ObservableProperty] uint _kcal;
    [ObservableProperty] uint _proteins;
    [ObservableProperty] uint _carbs;
    [ObservableProperty] uint _fats;


    public AddCustomIngredientViewModel(IngredientsService ingredientsService, MealComponentService mealComponentService, AIService aIService)
    {
        _ingredientsService = ingredientsService;
        _mealComponentService = mealComponentService;
        _aIService = aIService;
    }

    [RelayCommand]
    async void AutoFill()
    {
        if(string.IsNullOrWhiteSpace(Name))
        {
            return;
        }
        var resoult = await _aIService.AutoCalculateIngredientmMakroAsync(Name);
        if(resoult.success)
        {
            try
            {
                var makroValues = JsonConvert.DeserializeObject<IngriedentsModel>(resoult.message);
                Kcal = (uint)makroValues.Kcal;
                Proteins = (uint)makroValues.Proteins;
                Carbs = (uint)makroValues.Carbs;
                Fats = (uint)makroValues.Fats;
            }
            catch(Exception e)
            {
                //ignore error
            }
        }
    }

    [RelayCommand]
    private async void AddIngredient()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await Application.Current.MainPage.ShowPopupAsync(new MessagePopup("Please enter a valid name for the ingredient."));
            return;
        }
        if(Kcal <=0 || Proteins <0 || Carbs <0 || Fats <0)
        {
            await Application.Current.MainPage.ShowPopupAsync(new MessagePopup("Please enter valid nutritional values."));
            return;
        }
        IngriedentsModel newIngredient = new IngriedentsModel
        {
            Name = Name,
            Kcal = (int)Kcal,
            Proteins = (int)Proteins,
            Carbs = (int)Carbs,
            Fats = (int)Fats
        };
        var resoult = await _ingredientsService.AddCustomIngredientAsync(newIngredient); //adding ingredient to databse
        if (!resoult.success)
        {
            await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(resoult.message));
            return;
        }
        else
        {
            try
            {
                int newIngredientId = int.Parse(resoult.message); //getting the id of the newly added ingredient

                if (Weight > 0) //adding ingredient to the meal on phone and in DB if weight is greater than 0
                {
                    FoodModel newFood = new FoodModel
                    {
                        Id = newIngredientId,
                        Weight = Weight,
                        Name = Name,
                        Kcal = (int)Kcal,
                        Proteins = (int)Proteins,
                        Carbs = (int)Carbs,
                        Fats = (int)Fats
                    };
                    MealComponentDTO newMealComponent = new MealComponentDTO
                    {
                        MealId = _addFoodViewModel.CurrentMeal.Id,
                        IngredientId = newFood.Id,
                        Weight = Weight
                    };
                    var addFoodResoult = await _mealComponentService.AddMealComponentAsync(newMealComponent);
                    if (addFoodResoult.success)
                    {
                        _addFoodViewModel.CurrentMeal.Ingredients.Add(newFood);
                        await Shell.Current.GoToAsync("../..");
                    }
                    else
                    {
                        await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(addFoodResoult.message));
                        Weight = 0; //seting weight to 0 so second if will add this ingriednt to ingrident search list
                    }
                }
                if(Weight == 0) //adding ingredient to ingrident search list on phone
                {
                    _addFoodViewModel.Ingredients.Add(newIngredient);
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
