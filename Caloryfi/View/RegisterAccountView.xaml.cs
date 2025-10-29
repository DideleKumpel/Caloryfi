using Caloryfi.ViewModel;

namespace Caloryfi.View;

public partial class RegisterAccountView : ContentPage
{
	public RegisterAccountView(RegisterAccountViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}