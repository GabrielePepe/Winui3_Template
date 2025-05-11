using DevWinUI;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using ColorHelper = Microsoft.UI.ColorHelper;

namespace Winui3_Template
{
    public partial class App : Application
    {
        public ThemeService ThemeService { get; private set; }

        private Window? m_window;

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();

            ThemeService = new ThemeService(m_window);

            if (m_window.Content is FrameworkElement rootElement)
            {
                rootElement.ActualThemeChanged += RootElement_ActualThemeChanged;
            }

            UpdateCaptionButtonColors();

            m_window.Activate();
        }

        private void RootElement_ActualThemeChanged(FrameworkElement sender, object args)
        {
            UpdateCaptionButtonColors();
        }

        private void UpdateCaptionButtonColors()
        {
            var hWnd = WindowNative.GetWindowHandle(m_window);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            var titleBar = appWindow.TitleBar;

            if (m_window.Content is FrameworkElement fe)
            {
                var isDark = fe.ActualTheme == ElementTheme.Dark;

                titleBar.ButtonForegroundColor = isDark ? Colors.White : Colors.Black;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonHoverBackgroundColor = isDark
                    ? ColorHelper.FromArgb(20, 255, 255, 255)
                    : ColorHelper.FromArgb(20, 0, 0, 0);
                titleBar.ButtonPressedBackgroundColor = isDark
                    ? ColorHelper.FromArgb(40, 255, 255, 255)
                    : ColorHelper.FromArgb(40, 0, 0, 0);
            }
        }

        public ThemeService GetThemeService()
        {
            return ThemeService;
        }
    }
}
