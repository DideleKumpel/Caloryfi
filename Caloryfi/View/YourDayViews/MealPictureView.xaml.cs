
using Caloryfi.Model;
using Caloryfi.ViewModel.YourDayViewModels;

namespace Caloryfi.View.YourDayViews;

[QueryProperty(nameof(CurrentMealModel), "CurrentMealModel")]
public partial class MealPictureView : ContentPage
{
    private readonly MealDetailsViewModel _viewModel;

    public MealModel CurrentMealModel
    {
        set
        {
            _viewModel.Meal = value;
        }
    }
    public MealPictureView(MealPictureViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}