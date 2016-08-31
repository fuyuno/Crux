using Windows.UI.Xaml.Controls;

using Crux.ViewModels.Search;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Crux.Views.Search
{
    /// <summary>
    ///     それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class SearchMainPage : Page
    {
        public SearchMainPageViewModel ViewModel => DataContext as SearchMainPageViewModel;

        public SearchMainPage()
        {
            InitializeComponent();
        }
    }
}