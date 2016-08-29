using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Crux.Models;

using Microsoft.Xaml.Interactivity;

namespace Crux.Behaviors
{
    internal class GridViewEuqalitySizeBehavior : Behavior<ItemsWrapGrid>
    {
        // (0, 200)
        // (0, 200, 250)
        private readonly Regex _parseRegex = new Regex(@"\((?<mw>[0-9]+),(?<w>[0-9]+)(,(?<h>[0-9]+))?\)", RegexOptions.Compiled);

        private int _height;

        private int _width;

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            UpdateSize(e.Size);
        }

        private void UpdateSize(Size size)
        {
            var width = size.Width;
            var adaptiveSizes = ParseToAdaptiveSize(AdaptiveSizeStr);
            foreach (var adaptiveSize in adaptiveSizes)
            {
                if (!(adaptiveSize.MinWindowWidth <= width))
                    continue;
                _width = adaptiveSize.Width;
                _height = adaptiveSize.Height;
                break;
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var size = e.NewSize;

            var maxColumn = Math.Floor(size.Width / _width);
            var adjustedWidth = _width + size.Width % _width / maxColumn;

            if (IsEnabledHeight)
                AssociatedObject.ItemHeight = _height + (adjustedWidth - _width);
            AssociatedObject.ItemWidth = adjustedWidth;
        }

        private List<AdaptiveSize> ParseToAdaptiveSize(string str)
        {
            var list = new List<AdaptiveSize>();
            foreach (Match match in _parseRegex.Matches(str))
                if (match.Groups["h"] == null)
                    list.Add(new AdaptiveSize(int.Parse(match.Groups["mw"].Value), int.Parse(match.Groups["w"].Value)));
                else
                    list.Add(new AdaptiveSize(int.Parse(match.Groups["mw"].Value), int.Parse(match.Groups["w"].Value),
                                              int.Parse(match.Groups["h"].Value)));

            return list.OrderByDescending(w => w.MinWindowWidth).ToList();
        }

        #region AdaptiveSizeStrProperty

        public static readonly DependencyProperty AdaptiveSizeStrProperty =
            DependencyProperty.Register(nameof(AdaptiveSizeStr), typeof(string), typeof(GridViewEuqalitySizeBehavior),
                                        new PropertyMetadata(""));

        public string AdaptiveSizeStr
        {
            get { return (string) GetValue(AdaptiveSizeStrProperty); }
            set { SetValue(AdaptiveSizeStrProperty, value); }
        }

        #endregion

        #region IsEnabledHeightProperty

        public static readonly DependencyProperty IsEnabledHeightProperty =
            DependencyProperty.Register(nameof(IsEnabledHeight), typeof(bool), typeof(GridViewEuqalitySizeBehavior),
                                        new PropertyMetadata(false));

        public bool IsEnabledHeight
        {
            get { return (bool) GetValue(IsEnabledHeightProperty); }
            set { SetValue(IsEnabledHeightProperty, value); }
        }

        #endregion

        #region Overrides of Behavior

        protected override void OnAttached()
        {
            base.OnAttached();
            Window.Current.SizeChanged += OnWindowSizeChanged;
            AssociatedObject.SizeChanged += OnSizeChanged;
            UpdateSize(new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height));
        }

        protected override void OnDetaching()
        {
            Window.Current.SizeChanged -= OnWindowSizeChanged;
            AssociatedObject.SizeChanged -= OnSizeChanged;
            base.OnDetaching();
        }

        #endregion
    }
}