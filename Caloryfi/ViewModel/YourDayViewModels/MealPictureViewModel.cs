using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caloryfi.ViewModel.YourDayViewModels
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
    public class MealPictureViewModel
    {
        // TODO: add meal to list
    }
}
