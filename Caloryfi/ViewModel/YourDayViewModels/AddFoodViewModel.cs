using Caloryfi.Model;
using Caloryfi.Model.DTO;
using Caloryfi.Service;
using Caloryfi.Service;
using Caloryfi.Views.DialogPopups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Caloryfi.ViewModel.YourDayViewModels;

public partial class AddFoodViewModel : ObservableObject
{
    private readonly IngredientsService _ingredientsService;
    private readonly MealComponentService _mealComponentService;

    private MealModel _curentMeal;
    public MealModel CurrentMeal
    {
        set { _curentMeal = value; }
    }

    [ObservableProperty]
    private bool _loadingIsVisible;
    [ObservableProperty]
    private bool _errorMessageVisible;

    [ObservableProperty]
    string _searchText;

    [ObservableProperty]
    bool _popupVisible;

    [ObservableProperty]
    IngriedentsModel _selectedIngredient;

    [ObservableProperty]
    string _enteredWeight;

    [ObservableProperty]
    private ObservableCollection<IngriedentsModel> _ingredients;

    public AddFoodViewModel(IngredientsService ingredientsService, MealComponentService mealComponentService)
    {
        //Ingredients = new ObservableCollection<IngriedentsModel>
        //{
        //    new IngriedentsModel{ Id=1, Name="Tomato", Carbs=3, Fats=1, Proteins=1, Kcal=20 },
        //    new IngriedentsModel{ Id=2, Name="Chicken Breast", Carbs=0, Fats=2, Proteins=22, Kcal=120 },
        //    new IngriedentsModel{ Id=3, Name="Rice", Carbs=28, Fats=1, Proteins=3, Kcal=130 }
        //};
        _ingredientsService = ingredientsService;
        _mealComponentService = mealComponentService;

        LoadingIsVisible = false;
        ErrorMessageVisible = false;
        LoadIngredients();
        
    }

    [RelayCommand]
    private async void AddIngredient(IngriedentsModel selectedIngredient)
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
                    MealId = _curentMeal.Id,
                    IngredientId = selectedIngredient.Id,
                    Weight = newWeight
                };
                var resoult = await _mealComponentService.AddMealComponentAsync(UpdatedMeal);
                if (!resoult.success)
                {
                    var ErrorPopup = await Application.Current.MainPage.ShowPopupAsync(new MessagePopup(resoult.message));
                }
                else
                {
                    FoodModel foodToAdd = new FoodModel
                    {
                        Id = selectedIngredient.Id,
                        Name = selectedIngredient.Name,
                        Kcal = selectedIngredient.Kcal,
                        Carbs = selectedIngredient.Carbs,
                        Proteins = selectedIngredient.Proteins,
                        Fats = selectedIngredient.Fats,
                        Weight = newWeight
                    };
                    _curentMeal.Ingredients.Add(foodToAdd);
                    await Shell.Current.GoToAsync("..");
                }
            }
        }
    }

    private async void LoadIngredients()
    {
        LoadingIsVisible = true;
        var resoult = await _ingredientsService.GetIngredientsAsync();
        if (resoult.success)
        {
            Ingredients = JsonConvert.DeserializeObject<ObservableCollection<IngriedentsModel>>(resoult.message);
            LoadingIsVisible = false;
        }
        else
        {
            LoadingIsVisible = false;
            ErrorMessageVisible = true;
        }
    }

}
