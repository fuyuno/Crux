using Crux.Mvvm;

using Mntone.Nico2.Live.OnAirStreams;

using Prism.Windows.Navigation;

namespace Crux.ViewModels.Controls
{
    public class BroadcastProgramViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly OnAirStream _onAirStream;

        public string ThumbnailUrl => _onAirStream.SmallThumbnailUrl.ToString();

        public string Title => _onAirStream.Title;

        public string ViewCount => _onAirStream.ViewCount.HasValue ? string.Format("{0:#,#}", _onAirStream.ViewCount) : "-";

        public BroadcastProgramViewModel(INavigationService navigationService, OnAirStream onAirStream)
        {
            _navigationService = navigationService;
            _onAirStream = onAirStream;
        }

        public void OnItemTapped() => _navigationService.Navigate("Live", _onAirStream.Id);
    }
}