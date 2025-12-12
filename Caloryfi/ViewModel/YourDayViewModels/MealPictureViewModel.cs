using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel.YourDayViewModels;

public partial class MealPictureViewModel : ObservableObject
{
    [ObservableProperty] string name;
    [ObservableProperty] string weight;

    [ObservableProperty] string kcal;
    [ObservableProperty] string proteins;
    [ObservableProperty] string carbs;
    [ObservableProperty] string fats;

    public MealPictureViewModel()
    {
        // TODO: add meal to list
    }

    [RelayCommand]
    void LoadPicture()
    {
        // TODO: open picture, AI recognition etc.
    }

    [RelayCommand]
    void AddMeal()
    {

    }
    
}
