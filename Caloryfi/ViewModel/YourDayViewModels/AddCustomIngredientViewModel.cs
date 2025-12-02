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

public partial class AddCustomIngredientViewModel : ObservableObject
{
    [ObservableProperty] string _name;
    [ObservableProperty] int _weight;
    [ObservableProperty] int _kcal;
    [ObservableProperty] int _proteins;
    [ObservableProperty] int _carbs;
    [ObservableProperty] int _fats;

    [RelayCommand]
    void AutoFill()
    {
        // TODO: autofill from database
    }

    [RelayCommand]
    void AddIngredient()
    {
        // TODO: save ingredient
    }
}
