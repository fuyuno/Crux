using Crux.Mvvm;

using Mntone.Nico2.Live.Bookmark;

namespace Crux.ViewModels.Controls
{
    public class BroadcastProgramMiniViewModel : ViewModel
    {
        private readonly BookmarkStream _bookmarkStream;

        public string ThumbnailUrl => _bookmarkStream.CommunityInfo.Thumbnail;

        public string Title { get; }

        public BroadcastProgramMiniViewModel(BookmarkStream bookmarkStream)
        {
            _bookmarkStream = bookmarkStream;
            Title = _bookmarkStream.Title.Length >= 40 ? $"{_bookmarkStream.Title.Substring(0, 39)}..." : _bookmarkStream.Title;
        }
    }
}