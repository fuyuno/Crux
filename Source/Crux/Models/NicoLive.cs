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
        private readonly NiconicoContext _context;
        private uint _counter;
        private ushort _zIndex;
        public ObservableCollection<OnAirStream> LiveBroadcasts { get; }

        public NicoLive(NiconicoContext context)
        {
            _context = context;
            _zIndex = 1;
            LiveBroadcasts = new ObservableCollection<OnAirStream>();
            HasMoreItems = true;
        }

        private async Task SyncLives()
        {
            var broadcasts = await _context.Live.GetOnAirStreamsIndexAsync(_zIndex++);
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
                await SyncLives();
                return new LoadMoreItemsResult {Count = _counter};
            }).AsAsyncOperation();
        }

        public bool HasMoreItems { get; private set; }

        #endregion
    }
}