using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Crux.Collections;
using Crux.ViewModels.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Crux.Views.Controls
{
    public sealed partial class BroadcastList : UserControl
    {
        public BroadcastList()
        {
            InitializeComponent();
        }

        #region Header

        public static readonly DependencyProperty HeaderProperty
            = DependencyProperty.Register(nameof(Header), typeof(FrameworkElement), typeof(BroadcastList),
                                          new PropertyMetadata(null, HeaderPropertyChangedCallback));

        public FrameworkElement Header
        {
            get { return (FrameworkElement) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        private static void HeaderPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var obj = sender as BroadcastList;
            if (obj != null)
                obj.Header = (FrameworkElement) e.NewValue;
        }

        #endregion

        #region ItemsSource

        public static readonly DependencyProperty ItemsSourceProperty
            = DependencyProperty.Register(nameof(ItemsSource), typeof(IncrementalObservableCollection<BroadcastProgramViewModel>),
                                          typeof(BroadcastList), new PropertyMetadata(null, ItemsSourcePropertyChangedCallback));

        public IncrementalObservableCollection<BroadcastProgramViewModel> ItemsSource
        {
            get { return (IncrementalObservableCollection<BroadcastProgramViewModel>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsSourcePropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var obj = sender as BroadcastList;
            if (obj != null)
                obj.ItemsSource = (IncrementalObservableCollection<BroadcastProgramViewModel>) e.NewValue;
        }

        #endregion
    }
}