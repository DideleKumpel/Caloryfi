
using Caloryfi.Model;
using Caloryfi.ViewModel.YourDayViewModels;

namespace Caloryfi.View.YourDayViews;

[QueryProperty(nameof(AddFoodViewModel), "AddFoodViewModel")]
public partial class MealPictureView : ContentPage
{
    private readonly MealPictureViewModel _viewModel;

    public AddFoodViewModel AddFoodViewModel
    {
        set
        {
            _viewModel.AddFoodViewModel = value;
        }
    }
    public MealPictureView(MealPictureViewModel vm)
	{
        _viewModel = vm;
        BindingContext = vm;
		InitializeComponent();
	}
}