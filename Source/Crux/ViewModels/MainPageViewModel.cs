﻿using System.Collections.Generic;

using Crux.Collections;
using Crux.Helpers;
using Crux.Models;
using Crux.Mvvm;
using Crux.Services.Interfaces;
using Crux.ViewModels.Controls;

using Prism.Windows.Navigation;

namespace Crux.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IAccountService _accountService;
        private readonly INavigationService _navigationService;
        private readonly NicoLive _nicoLive;

        public IncrementalObservableCollection<BroadcastProgramViewModel> Broadcasts { get; }

        public MainPageViewModel(IAccountService accountService, INavigationService navigationService)
        {
            _accountService = accountService;
            _navigationService = navigationService;
            _nicoLive = new NicoLive(_accountService.CurrentContext);
            Broadcasts = new IncrementalObservableCollection<BroadcastProgramViewModel>();
        }

        #region Overrides of ViewModelBase

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            _nicoLive.LiveBroadcasts.Clear();
            ModelHelper.ConnectTo(Broadcasts, _nicoLive, w => w.LiveBroadcasts, w => new BroadcastProgramViewModel(_navigationService, w));
            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion
    }
}