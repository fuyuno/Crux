using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Crux
{
    /// <summary>
    ///     それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class AppShell : Page, INotifyPropertyChanged
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetContentFrame(Frame frame)
        {
            RootGrid.Children.Insert(0, frame);
            AppRootFrame = frame;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region AppRootFrame

        private Frame _appRootFrame;

        public Frame AppRootFrame
        {
            get { return _appRootFrame; }
            set
            {
                if (_appRootFrame == value)
                    return;
                _appRootFrame = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}