
using Caloryfi.ViewModel.YourDayViewModels;

namespace Caloryfi.View.YourDayViews;

public partial class MealPictureView : ContentPage
{
	public MealPictureView(MealPictureViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}