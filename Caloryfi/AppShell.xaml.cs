using Caloryfi.View.ProfileViews;

namespace Caloryfi
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        }
    }
}
