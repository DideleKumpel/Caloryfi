using Caloryfi.Model;
using Caloryfi.Model.DTO;
using Caloryfi.Service;
using Caloryfi.Service;
using Caloryfi.View.YourDayViews;
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
        set { _curentMeal = value; } get { return _curentMeal; }
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
    private ObservableCollection<IngriedentsModel> _ingredientsDispalay;

    private ObservableCollection<IngriedentsModel> _ingredients;

    public ObservableCollection<IngriedentsModel> Ingredients
    {
        get { return _ingredients; } set { _ingredients = value; }
    }

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

    [RelayCommand]
    private void SearchIngredients()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            IngredientsDispalay = new ObservableCollection<IngriedentsModel>(_ingredients);
        }
        else
        {
            IngredientsDispalay.Clear();
            var lowerSearchText = SearchText.ToLower();
            foreach (var ingredient in _ingredients)
            {
                bool isVisible = ingredient.Name.ToLower().Contains(lowerSearchText);
                if (isVisible)
                {
                    IngredientsDispalay.Add(ingredient);
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
            _ingredients = JsonConvert.DeserializeObject<ObservableCollection<IngriedentsModel>>(resoult.message);
            LoadingIsVisible = false;
            IngredientsDispalay = new ObservableCollection<IngriedentsModel>(_ingredients);
        }
        else
        {
            LoadingIsVisible = false;
            ErrorMessageVisible = true;
        }
    }
    [RelayCommand]
    private async void GoToCustomIngredient()
    {
        await Shell.Current.GoToAsync(nameof(View.YourDayViews.AddCustomIngredientView), new Dictionary<string, object>
            {
                { "AddFoodViewModel", this }
            });
    }
}
