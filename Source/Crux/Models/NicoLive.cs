using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.UI.Xaml.Data;

using Microsoft.Practices.ObjectBuilder2;

using Mntone.Nico2;
using Mntone.Nico2.Live.OnAirStreams;

namespace Crux.Models
{
    public class NicoLive : ISupportIncrementalLoading
    {
        private readonly Category _category;
        private readonly NiconicoContext _context;
        private uint _counter;
        private LiveSort _sort;
        private ushort _zIndex;
        public ObservableCollection<OnAirStream> LiveBroadcasts { get; }

        public NicoLive(NiconicoContext context)
        {
            _context = context;
            _category = Category.General;
            _sort = null;
            _zIndex = 1;
            LiveBroadcasts = new ObservableCollection<OnAirStream>();
            HasMoreItems = true;
        }

        public NicoLive(NiconicoContext context, Category category, LiveSort sort)
        {
            _context = context;
            _category = category;
            _sort = sort;
            _zIndex = 1;
            LiveBroadcasts = new ObservableCollection<OnAirStream>();
            HasMoreItems = true;
        }

        public void Query(LiveSort sort)
        {
            _sort = sort;
            _zIndex = 1;
            LiveBroadcasts.Clear();
            HasMoreItems = true;
        }

        private async Task SyncBroadcasts()
        {
            OnAirStreamsResponse broadcasts;
            if (_sort == null)
                broadcasts = await _context.Live.GetOnAirStreamsIndexAsync(_zIndex++);
            else
                broadcasts = await _context.Live.GetOnAirStreamsRecentAsync(_zIndex++, _category, _sort.SortDirection, _sort.SortType);
            broadcasts.OnAirStreams.ForEach(w => LiveBroadcasts.Add(w));
            _counter = (uint) broadcasts.OnAirStreams.Count;
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