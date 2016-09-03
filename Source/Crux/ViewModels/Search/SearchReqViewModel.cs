﻿using System;

using Crux.Collections;
using Crux.Helpers;
using Crux.Models;
using Crux.Services.Interfaces;
using Crux.ViewModels.Controls;

using Mntone.Nico2.Live.OnAirStreams;

namespace Crux.ViewModels.Search
{
    public class SearchReqViewModel : SearchBaseViewModel
    {
        private readonly NicoLive _nicoLive;
        public IncrementalObservableCollection<BroadcastProgramViewModel> Broadcasts { get; }

        public SearchReqViewModel(IAccountService accountService)
        {
            _nicoLive = new NicoLive(accountService.CurrentContext, Category.Introduction, SelectedItem);
            Broadcasts = new IncrementalObservableCollection<BroadcastProgramViewModel>();
            ModelHelper.ConnectTo(Broadcasts, _nicoLive, w => w.LiveBroadcasts, w => new BroadcastProgramViewModel(w));
            SelectedIndex.Subscribe(w => _nicoLive.Query(SelectedItem));
        }
    }
}