using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Microsoft.Xaml.Interactivity;

namespace Crux.Behaviors
{
    internal class MediaElementBindingBehavior : Behavior<MediaElement>
    {
        #region MediaStreamSource

        public static readonly DependencyProperty MediaStreamSourceProperty =
            DependencyProperty.Register(nameof(MediaStreamSource), typeof(IMediaSource), typeof(MediaElementBindingBehavior),
                                        new PropertyMetadata(null, MediaStreamSourceChangedCallback));

        public IMediaSource MediaStreamSource
        {
            get { return (IMediaSource) GetValue(MediaStreamSourceProperty); }
            set { SetValue(MediaStreamSourceProperty, value); }
        }

        private static void MediaStreamSourceChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var mediaElementBehavior = sender as MediaElementBindingBehavior;
            if (mediaElementBehavior == null)
                return;

            var mediaElement = mediaElementBehavior.AssociatedObject;
            mediaElement.SetMediaStreamSource(mediaElementBehavior.MediaStreamSource);
            mediaElement.MediaEnded += (sender2, e2) => mediaElementBehavior.IsLiving = false;
            mediaElement.Play();
            mediaElementBehavior.IsLiving = true;
        }

        #endregion

        #region IsLiving

        public static readonly DependencyProperty IsLivingProperty =
            DependencyProperty.Register(nameof(IsLiving), typeof(bool), typeof(MediaElement), new PropertyMetadata(false));

        public bool IsLiving
        {
            get { return (bool) GetValue(IsLivingProperty); }
            set { SetValue(IsLivingProperty, value); }
        }

        #endregion
    }
}