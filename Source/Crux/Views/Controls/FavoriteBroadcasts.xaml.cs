using Windows.UI.Xaml.Controls;

using Crux.ViewModels.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Crux.Views.Controls
{
    public sealed partial class FavoriteBroadcasts : UserControl
    {
        public FavoriteBroadcastsViewModel ViewModel => DataContext as FavoriteBroadcastsViewModel;

        public FavoriteBroadcasts()
        {
            InitializeComponent();
        }
    }
}