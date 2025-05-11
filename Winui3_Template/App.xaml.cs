using DevWinUI;
using Microsoft.UI.Xaml;

namespace Winui3_Template
{
    public partial class App : Application
    {
        public ThemeService ThemeService { get; private set; }

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();

            ThemeService = new ThemeService(m_window);

            m_window.Activate();
        }

        private Window? m_window;

        public ThemeService GetThemeService()
        {
            return ThemeService;
        }
    }
}
