using Windows.UI.Xaml.Controls;

using Crux.ViewModels.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Crux.Views.Controls
{
    public sealed partial class CurrentUser : UserControl
    {
        public CurrentUserViewModel ViewModel => DataContext as CurrentUserViewModel;

        public CurrentUser()
        {
            InitializeComponent();
        }
    }
}