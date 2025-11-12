using Caloryfi.ViewModel.ProfileViewModels;

namespace Caloryfi.View.ProfileViews;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vs)
	{
		BindingContext = vs;
        InitializeComponent();
	}
}