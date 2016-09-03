using System;
using System.Collections.Generic;

using Crux.Collections;
using Crux.Helpers;
using Crux.Models;
using Crux.Mvvm;
using Crux.Services.Interfaces;
using Crux.ViewModels.Controls;

using Mntone.Nico2.Live.OnAirStreams;

using Prism.Windows.Navigation;

using Reactive.Bindings;

namespace Crux.ViewModels.Search
{
    public class SearchBaseViewModel : ViewModel
    {
        public IEnumerable<LiveSort> SortTypes => LiveSort.Sorts;

        public ReactiveProperty<int> SelectedIndex { get; }
        public IncrementalObservableCollection<BroadcastProgramViewModel> Broadcasts { get; }

        protected LiveSort SelectedItem => LiveSort.Sorts[SelectedIndex.Value];
        protected NicoLive NicoLive { get; }

        protected SearchBaseViewModel(IAccountService accountService, INavigationService navigationService, Category category)
        {
            Broadcasts = new IncrementalObservableCollection<BroadcastProgramViewModel>();
            SelectedIndex = new ReactiveProperty<int>(0);
            NicoLive = new NicoLive(accountService.CurrentContext, category, SelectedItem);
            ModelHelper.ConnectTo(Broadcasts, NicoLive, w => w.LiveBroadcasts, w => new BroadcastProgramViewModel(navigationService, w));
            SelectedIndex.Subscribe(w => NicoLive.Query(SelectedItem));
        }
    }
}