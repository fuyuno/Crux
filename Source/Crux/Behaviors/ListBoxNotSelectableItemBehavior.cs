using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Microsoft.Xaml.Interactivity;

namespace Crux.Behaviors
{
    public class ListBoxNotSelectableItemBehavior : Behavior<ListBox>
    {
        private void AssociatedObjectOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = AssociatedObject.SelectedItem as ListBoxItem;
            if (!ListBoxNotSelectableItem.GetMark(item))
                return;
            AssociatedObject.SelectionChanged -= AssociatedObjectOnSelectionChanged;
            var index = AssociatedObject.Items?.IndexOf(e.RemovedItems[0] as ListBoxItem) ?? 0;
            AssociatedObject.SelectedIndex = index;
            AssociatedObject.SelectionChanged += AssociatedObjectOnSelectionChanged;
        }

        #region Overrides of Behavior<ListBox>

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObjectOnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= AssociatedObjectOnSelectionChanged;
            base.OnDetaching();
        }

        #endregion
    }

    public static class ListBoxNotSelectableItem
    {
        #region Mark

        public static readonly DependencyProperty MarkProperty =
            DependencyProperty.RegisterAttached("Mark", typeof(bool), typeof(ListBoxNotSelectableItemBehavior), new PropertyMetadata(false));

        public static bool GetMark(DependencyObject obj) => (bool) obj.GetValue(MarkProperty);

        public static void SetMark(DependencyObject obj, bool value) => obj.SetValue(MarkProperty, value);

        #endregion
    }
}