using Crux.Mvvm;

using Mntone.Nico2.Live.Bookmark;

using Prism.Windows.Navigation;

namespace Crux.ViewModels.Controls
{
    public class BroadcastProgramMiniViewModel : ViewModel
    {
        private readonly BookmarkStream _bookmarkStream;
        private readonly INavigationService _navigationService;

        public string ThumbnailUrl => _bookmarkStream.CommunityInfo.Thumbnail;

        public string Title { get; }

        public BroadcastProgramMiniViewModel(INavigationService navigationService, BookmarkStream bookmarkStream)
        {
            _navigationService = navigationService;
            _bookmarkStream = bookmarkStream;
            Title = _bookmarkStream.Title.Length >= 40 ? $"{_bookmarkStream.Title.Substring(0, 39)}..." : _bookmarkStream.Title;
        }

        public void OnItemTapped() => _navigationService.Navigate("Live", $"lv{_bookmarkStream.Id}");
    }
}