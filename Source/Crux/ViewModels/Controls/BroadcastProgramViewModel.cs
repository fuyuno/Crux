using Crux.Mvvm;

using Mntone.Nico2.Live.OnAirStreams;

namespace Crux.ViewModels.Controls
{
    public class BroadcastProgramViewModel : ViewModel
    {
        private readonly OnAirStream _onAirStream;

        public string ThumbnailUrl => _onAirStream.SmallThumbnailUrl.ToString().Replace("/s/", "/");

        public string Title => _onAirStream.Title;

        public string ViewCount => _onAirStream.ViewCount.HasValue ? string.Format("{0:#,#}", _onAirStream.ViewCount) : "-";

        public BroadcastProgramViewModel(OnAirStream onAirStream)
        {
            _onAirStream = onAirStream;
        }
    }
}