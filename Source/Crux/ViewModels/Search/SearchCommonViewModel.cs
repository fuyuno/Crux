using System;

using Crux.Collections;
using Crux.Helpers;
using Crux.Models;
using Crux.Services.Interfaces;
using Crux.ViewModels.Controls;

using Mntone.Nico2.Live.OnAirStreams;

namespace Crux.ViewModels.Search
{
    public class SearchCommonViewModel : SearchBaseViewModel
    {
        private readonly NicoLive _nicoLive;
        public IncrementalObservableCollection<BroadcastProgramViewModel> Broadcasts { get; }

        public SearchCommonViewModel(IAccountService accountService)
        {
            _nicoLive = new NicoLive(accountService.CurrentContext, Category.General, SelectedItem);
            Broadcasts = new IncrementalObservableCollection<BroadcastProgramViewModel>();
            ModelHelper.ConnectTo(Broadcasts, _nicoLive, w => w.LiveBroadcasts, w => new BroadcastProgramViewModel(w));
            SelectedIndex.Subscribe(w => _nicoLive.Query(SelectedItem));
        }
    }
}