using Caloryfi.ViewModel.HistoryViewModels;

namespace Caloryfi.View.HistoryViews;

public partial class HistoryView : ContentPage
{
	public HistoryView(HistoryViewModel vm)
	{
		BindingContext = vm;
        InitializeComponent();
	}
}