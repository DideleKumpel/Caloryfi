using Caloryfi.ViewModel.YourDayViewModels;

namespace Caloryfi.View.YourDayViews;

public partial class YourDayView : ContentPage
{
    public YourDayView(YourDayViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as YourDayViewModel)?.CalculateTotals();
    }
}