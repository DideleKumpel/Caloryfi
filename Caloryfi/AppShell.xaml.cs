using Caloryfi.View.ProfileViews;
using Caloryfi.View.YourDayViews;

namespace Caloryfi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
            Routing.RegisterRoute(nameof(MealDetailsView), typeof(MealDetailsView));
            Routing.RegisterRoute(nameof(AddFoodView), typeof(AddFoodView));
            Routing.RegisterRoute(nameof(AddCustomIngredientView), typeof(AddCustomIngredientView));
        }
    }
}
