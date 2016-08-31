using Windows.UI.Xaml.Controls;

using Crux.ViewModels.Search;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Crux.Views.Search
{
    public sealed partial class SearchFaceView : UserControl
    {
        public SearchFaceViewModel ViewModel => DataContext as SearchFaceViewModel;

        public SearchFaceView()
        {
            InitializeComponent();
        }
    }
}