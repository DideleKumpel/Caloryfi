using Caloryfi.Model;
using Caloryfi.ViewModel.HistoryViewModels;

namespace Caloryfi.View.HistoryViews;

[QueryProperty(nameof(CurrentMealModel), "CurrentMealModel")]
public partial class HistoryMealDetailView : ContentPage
{
    private readonly HistoryMealDetailViewModel _viewModel;

    public MealModel CurrentMealModel
    {
        set
        {
            _viewModel.Meal = value;
        }
    }
    public HistoryMealDetailView(HistoryMealDetailViewModel vm)
	{
		InitializeComponent();
        _viewModel = vm;
        BindingContext = vm;
    }
}