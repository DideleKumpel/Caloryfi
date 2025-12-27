using Caloryfi.View;
using Caloryfi.View.HistoryViews;
using Caloryfi.View.YourDayViews;

namespace Caloryfi
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //var loginView = _serviceProvider.GetRequiredService<LoginView>();
            var loginView = _serviceProvider.GetRequiredService<HistoryView>();
            return new Window(loginView);
        }
    }
}