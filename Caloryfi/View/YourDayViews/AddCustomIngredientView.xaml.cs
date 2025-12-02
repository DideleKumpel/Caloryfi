using Caloryfi.Model;
using Caloryfi.ViewModel.YourDayViewModels;
namespace Caloryfi.View.YourDayViews;

public partial class AddCustomIngredientView : ContentPage
{

    public AddCustomIngredientView(AddCustomIngredientViewModel cm)
    {
        InitializeComponent();
        BindingContext = cm;
    }
}