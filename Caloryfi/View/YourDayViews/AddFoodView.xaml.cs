using Caloryfi.Model;
using Caloryfi.ViewModel.YourDayViewModels;
namespace Caloryfi.View.YourDayViews;

[QueryProperty(nameof(CurrentMealModel), "CurrentMealModel")]
public partial class AddFoodView : ContentPage
{
    private readonly AddFoodViewModel _viewModel;

    public MealModel CurrentMealModel
    {
        set
        {
            _viewModel.CurrentMeal = value;
        }
    }
    public AddFoodView(AddFoodViewModel fm)
	{
		InitializeComponent();
        _viewModel = fm;
        BindingContext = fm;
	}
}