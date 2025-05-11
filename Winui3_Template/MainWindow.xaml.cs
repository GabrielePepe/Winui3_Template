using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;


namespace Winui3_Template
{
    public sealed partial class MainWindow : Window
    {
        private Stack<Type> navigationHistory = new Stack<Type>();

        public MainWindow()
        {
            this.InitializeComponent();

            SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.Base };

            this.ExtendsContentIntoTitleBar = true;

            SetTitleBar(AppTitleBar);

            this.AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

            nvSample.IsPaneOpen = false;

            NavigateToPage(typeof(HomePage));

            nvSample.ItemInvoked += NvSample_ItemInvoked;

            nvSample.BackRequested += NvSample_BackRequested;

            contentFrame.Navigated += ContentFrame_Navigated;
        }

        public string GetAppTitleFromSystem()
        {
            return Windows.ApplicationModel.Package.Current.DisplayName;
        }

        private void NvSample_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigateToPage(typeof(SettingsPage));
            }
            else
            {
                var selectedItem = (NavigationViewItem)args.InvokedItemContainer;
                switch (selectedItem.Tag.ToString())
                {
                    case "HomePage":
                        NavigateToPage(typeof(HomePage));
                        break;
                }
            }
        }

        private void NvSample_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (navigationHistory.Count > 0)
            {
                var previousPage = navigationHistory.Pop();

                contentFrame.Navigate(previousPage);

                UpdateNavigationSelection(previousPage);
            }
            nvSample.IsBackEnabled = navigationHistory.Count > 0;
        }

        private void NavigateToPage(Type pageType)
        {
            if (contentFrame.Content?.GetType() != pageType)
            {
                if (contentFrame.Content != null)
                {
                    navigationHistory.Push(contentFrame.Content.GetType());
                }
                contentFrame.Navigate(pageType);
            }

            nvSample.IsBackEnabled = navigationHistory.Count > 0;
            UpdateNavigationSelection(pageType);
        }

        private void UpdateNavigationSelection(Type pageType)
        {
            if (pageType == typeof(SettingsPage))
            {
                nvSample.SelectedItem = nvSample.SettingsItem;
            }
            else
            {
                var selectedItem = nvSample.MenuItems.OfType<NavigationViewItem>()
                    .FirstOrDefault(item => item.Tag?.ToString() == pageType.Name);

                if (selectedItem != null)
                {
                    nvSample.SelectedItem = selectedItem;
                }
                else
                {
                    nvSample.SelectedItem = null;
                }
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateNavigationSelection(e.SourcePageType);

            nvSample.IsBackEnabled = navigationHistory.Count > 0;
        }
    }
}
