using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Caloryfi.ViewModel.YourDayViewModels;

public partial class MealPictureViewModel : ObservableObject
{
    [ObservableProperty] string name;
    [ObservableProperty] string weight;

    [ObservableProperty] string kcal;
    [ObservableProperty] string proteins;
    [ObservableProperty] string carbs;
    [ObservableProperty] string fats;

    [RelayCommand]
    void LoadPicture()
    {
        // TODO: open picture, AI recognition etc.
    }

    [RelayCommand]
    void AddMeal()
    {
        // TODO: add meal to list
    }
}
