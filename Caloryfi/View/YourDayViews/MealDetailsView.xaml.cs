using Caloryfi.Model;
using Caloryfi.ViewModel.YourDayViewModels;

namespace Caloryfi.View.YourDayViews;

[QueryProperty(nameof(CurrentMealModel), "CurrentMealModel")]
public partial class MealDetailsView : ContentPage
{
	private readonly MealDetailsViewModel _viewModel;
	
	public MealModel CurrentMealModel
    { 
		set {
			_viewModel.Meal = value;
        } 
	}
	public MealDetailsView(MealDetailsViewModel vm)
	{
        InitializeComponent();
        _viewModel = vm;
        BindingContext = vm;
    }
}