using Caloryfi.ViewModel.ProfileViewModels;

namespace Caloryfi.View.ProfileViews;

public partial class SettingsView : ContentPage
{
	public SettingsView( SettingsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}