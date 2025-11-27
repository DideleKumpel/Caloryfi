using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Caloryfi.Model;

namespace Caloryfi.ViewModel.YourDayViewModels;

public partial class AddFoodViewModel : ObservableObject
{
    [ObservableProperty]
    string _searchText;

    [ObservableProperty]
    bool _popupVisible;

    [ObservableProperty]
    IngriedentsModel _selectedIngredient;

    [ObservableProperty]
    string _enteredWeight;

    public ObservableCollection<IngriedentsModel> Ingredients { get; set; }

    public AddFoodViewModel()
    {
        Ingredients = new ObservableCollection<IngriedentsModel>
        {
            new IngriedentsModel{ Id=1, Name="Tomato", Carbs=3, Fats=1, Proteins=1, Kcal=20 },
            new IngriedentsModel{ Id=2, Name="Chicken Breast", Carbs=0, Fats=2, Proteins=22, Kcal=120 },
            new IngriedentsModel{ Id=3, Name="Rice", Carbs=28, Fats=1, Proteins=3, Kcal=130 }
        };
    }

    //[RelayCommand]
    //void GoToMealPicture()
    //{
    //    Shell.Current.GoToAsync(nameof(View.YourDayViews.MealPictureView));
    //}

    //[RelayCommand]
    //void GoToCustomIngredient()
    //{
    //    Shell.Current.GoToAsync(nameof(View.YourDayViews.CustomIngredientView));
    //}

    //[RelayCommand]
    //void OpenAddWeightPopup(IngriedentsModel ingredient)
    //{
    //    SelectedIngredient = ingredient;
    //    PopupVisible = true;
    //}

    //[RelayCommand]
    //void ConfirmAddIngredient()
    //{
    //    // TODO: handle adding ingredient with weight
    //    PopupVisible = false;
    //    EnteredWeight = "";
    //}

    //[RelayCommand]
    //void CancelPopup()
    //{
    //    PopupVisible = false;
    //    EnteredWeight = "";
    //}
}
