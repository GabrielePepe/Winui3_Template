using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.System;
using DevWinUI;

namespace Winui3_Template
{
    public sealed partial class SettingsPage : Page
    {
        public ThemeService ThemeServiceProxy => ((App)Application.Current).GetThemeService();

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private async void SourceCodeButtonClick(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("http://www.microsoft.com");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
