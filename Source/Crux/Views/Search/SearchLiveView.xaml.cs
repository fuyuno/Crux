using Windows.UI.Xaml.Controls;

using Crux.ViewModels.Search;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Crux.Views.Search
{
    public sealed partial class SearchLiveView : UserControl
    {
        public SearchLiveViewModel ViewModel => DataContext as SearchLiveViewModel;

        public SearchLiveView()
        {
            InitializeComponent();
        }
    }
}