using System.Collections.Generic;

using Crux.Models;
using Crux.Mvvm;
using Crux.Services.Interfaces;

using Prism.Windows.Navigation;

namespace Crux.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IAccountService _accountService;
        private readonly NicoLive _nicoLive;

        public MainPageViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            _nicoLive = new NicoLive(_accountService.CurrentContext);
        }

        #region Overrides of ViewModelBase

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            _nicoLive.LiveBroadcasts.Clear();
            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion
    }
}