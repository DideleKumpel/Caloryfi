using Caloryfi.ViewModel.YourDayViewModels;

namespace Caloryfi.View.YourDayViews;

public partial class YourDayView : ContentPage
{
	public YourDayView(YourDayViewModel vm)
	{
		BindingContext = vm;
        InitializeComponent();
	}
}