using Windows.UI.Xaml.Controls;

using Crux.ViewModels.Search;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Crux.Views.Search
{
    public sealed partial class SearchCommonView : UserControl
    {
        public SearchCommonViewModel ViewModel => DataContext as SearchCommonViewModel;

        public SearchCommonView()
        {
            InitializeComponent();
        }
    }
}