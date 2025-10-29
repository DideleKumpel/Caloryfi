using Caloryfi.ViewModel;

namespace Caloryfi.View;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}