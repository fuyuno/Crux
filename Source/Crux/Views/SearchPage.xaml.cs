using Windows.UI.Xaml.Controls;

using Crux.ViewModels;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Crux.Views
{
    /// <summary>
    ///     それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public SearchPageViewModel ViewModel => DataContext as SearchPageViewModel;

        public SearchPage()
        {
            InitializeComponent();
        }
    }
}