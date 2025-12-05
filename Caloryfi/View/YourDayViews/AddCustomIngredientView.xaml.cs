using Caloryfi.Model;
using Caloryfi.ViewModel.YourDayViewModels;
namespace Caloryfi.View.YourDayViews;

[QueryProperty(nameof(AddFoodViewModel), "AddFoodViewModel")]
public partial class AddCustomIngredientView : ContentPage
{
    private readonly AddCustomIngredientViewModel _viewModel;

    public AddFoodViewModel AddFoodViewModel
    {
        set
        {
            _viewModel.AddFoodViewModel = value;
        }
    }
    public AddCustomIngredientView(AddCustomIngredientViewModel vm)
    {
        InitializeComponent();
        _viewModel = vm;
        BindingContext = vm;
    }
}