using Windows.UI.Xaml.Controls;

using Crux.ViewModels.Settings;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Crux.Views.Settings
{
    public sealed partial class SettingsAboutView : UserControl
    {
        public SettingsAboutViewModel ViewModel => DataContext as SettingsAboutViewModel;

        public SettingsAboutView()
        {
            InitializeComponent();
        }
    }
}