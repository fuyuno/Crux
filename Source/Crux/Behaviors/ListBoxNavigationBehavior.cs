using System;
using System.Collections.Generic;
using System.Globalization;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Crux.Helpers;

using Microsoft.Xaml.Interactivity;

namespace Crux.Behaviors
{
    internal class ListBoxNavigationBehavior : Behavior<ListBox>
    {
        private readonly Stack<int> _pageStack;
        private int _oldIndex;

        public ListBoxNavigationBehavior()
        {
            _pageStack = new Stack<int>();
        }

        private void AddEventHandler()
        {
            if (ContentFrame == null)
            {
                RunHelper.RunLater(AddEventHandler, TimeSpan.FromMilliseconds(100));
                return;
            }
            ContentFrame.Navigating += ContentFrameOnNavigating;
        }

        private void SyncSelectItem(NavigationMode navigationMode = NavigationMode.New)
        {
            if (navigationMode == NavigationMode.Back)
            {
                ContentFrame.GoBack();
                return;
            }
            var selectedItem = AssociatedObject.SelectedItem as ListBoxItem;
            var pageToken = ListBoxNavigation.GetPageToken(selectedItem);
            if (!string.IsNullOrWhiteSpace(pageToken))
                ContentFrame?.Navigate(GetPageType(pageToken));
            if (ParentSplitView != null)
                ParentSplitView.IsPaneOpen = false;
        }

        private Type GetPageType(string pageToken)
        {
            var assemblyQualifiedAppType = GetType().AssemblyQualifiedName;
            var pageNameWithParameter = assemblyQualifiedAppType.Replace(GetType().FullName, typeof(App).Namespace + ".Views.{0}Page");
            var viewFullName = string.Format(CultureInfo.InvariantCulture, pageNameWithParameter, pageToken);
            var viewType = Type.GetType(viewFullName);

            if (viewType == null)
                throw new ArgumentException($"PageType {viewFullName} is not found.");

            return viewType;
        }

        private void ContentFrameOnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (ContentFrame.BackStack.Count < _pageStack.Count)
                _pageStack.Pop();

            AssociatedObject.SelectionChanged -= ListBoxSelectionChanged;
            switch (e.NavigationMode)
            {
                case NavigationMode.Back:
                    AssociatedObject.SelectedIndex = _pageStack.Pop();
                    break;

                case NavigationMode.New:
                case NavigationMode.Forward:
                    _pageStack.Push(_oldIndex);
                    break;

                case NavigationMode.Refresh:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            AssociatedObject.SelectionChanged += ListBoxSelectionChanged;
            _oldIndex = AssociatedObject.SelectedIndex;
            SyncSelectItem(e.NavigationMode);
        }

        private void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            SyncSelectItem();
        }

        #region Overrides of Behavior

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= ListBoxSelectionChanged;
            if (ContentFrame != null)
                ContentFrame.Navigating -= ContentFrameOnNavigating;
            RunHelper.RunLater(AddEventHandler, TimeSpan.FromMilliseconds(100));
            base.OnDetaching();
        }

        #endregion

        #region Overrides of Behavior<ListBox>

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += ListBoxSelectionChanged;
            _oldIndex = 0;
            RunHelper.RunLater(AddEventHandler, TimeSpan.FromMilliseconds(500));
        }

        #endregion

        #region ContentFrame

        public static readonly DependencyProperty ContentFrameProperty = DependencyProperty.Register(nameof(ContentFrame), typeof(Frame),
                                                                                                     typeof(ListBoxNavigationBehavior),
                                                                                                     new PropertyMetadata(null));

        public Frame ContentFrame
        {
            get { return (Frame) GetValue(ContentFrameProperty); }
            set { SetValue(ContentFrameProperty, value); }
        }

        #endregion

        #region ParentSplitView

        public static readonly DependencyProperty ParentSplitViewProperty = DependencyProperty.Register(nameof(ParentSplitView),
                                                                                                        typeof(SplitView),
                                                                                                        typeof(ListBoxNavigationBehavior),
                                                                                                        new PropertyMetadata(null));

        public SplitView ParentSplitView
        {
            get { return (SplitView) GetValue(ParentSplitViewProperty); }
            set { SetValue(ParentSplitViewProperty, value); }
        }

        #endregion
    }

    public static class ListBoxNavigation
    {
        #region PageToken

        public static readonly DependencyProperty PageTokenProperty = DependencyProperty.RegisterAttached("PageToken", typeof(string),
                                                                                                          typeof(ListBoxNavigationBehavior),
                                                                                                          new PropertyMetadata(string.Empty));

        public static string GetPageToken(DependencyObject obj) => (string) obj.GetValue(PageTokenProperty);

        public static void SetPageToken(DependencyObject obj, string value) => obj.SetValue(PageTokenProperty, value);

        #endregion
    }
}