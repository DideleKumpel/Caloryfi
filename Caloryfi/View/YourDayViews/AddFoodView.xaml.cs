using Caloryfi.ViewModel.YourDayViewModels;
namespace Caloryfi.View.YourDayViews;

public partial class AddFoodView : ContentPage
{
	public AddFoodView(AddFoodViewModel fm)
	{
		InitializeComponent();
		BindingContext = fm;
	}
}