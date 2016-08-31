using Windows.UI.Xaml.Controls;

using Crux.ViewModels.Search;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Crux.Views.Search
{
    public sealed partial class SearchReqView : UserControl
    {
        public SearchReqViewModel ViewModel => DataContext as SearchReqViewModel;

        public SearchReqView()
        {
            InitializeComponent();
        }
    }
}