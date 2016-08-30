using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.UI.Xaml.Data;

using Mntone.Nico2;
using Mntone.Nico2.Live.Bookmark;

namespace Crux.Models
{
    internal class NicoFavLive : ISupportIncrementalLoading
    {
        private readonly NiconicoContext _context;
        private uint _counter;
        private int _page;

        public ObservableCollection<BookmarkStream> LiveBroadcasts { get; }

        public NicoFavLive(NiconicoContext context)
        {
            _context = context;
            _page = 1;
            LiveBroadcasts = new ObservableCollection<BookmarkStream>();
            HasMoreItems = true;
        }

        private async Task SyncBroadcasts()
        {
            var broadcasts = await _context.Live.GetBookmarksAsync(_page++);
            broadcasts.BookmarkStreams.ForEach(w => LiveBroadcasts.Add(w));
            _counter = (uint) broadcasts.BookmarkStreams.Count;
            if (_counter == 0)
                HasMoreItems = false;
        }

        #region Implementation of ISupportIncrementalLoading

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run(async () =>
            {
                await SyncBroadcasts();
                return new LoadMoreItemsResult {Count = _counter};
            }).AsAsyncOperation();
        }

        public bool HasMoreItems { get; private set; }

        #endregion
    }
}