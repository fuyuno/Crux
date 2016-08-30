using Crux.Collections;
using Crux.Helpers;
using Crux.Models;
using Crux.Mvvm;
using Crux.Services.Interfaces;

namespace Crux.ViewModels.Controls
{
    public class FavoriteBroadcastsViewModel : ViewModel
    {
        private readonly IAccountService _accountService;
        private readonly NicoFavLive _nicoFavLive;
        public IncrementalObservableCollection<BroadcastProgramMiniViewModel> Broadcasts { get; }

        public FavoriteBroadcastsViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            _nicoFavLive = new NicoFavLive(_accountService.CurrentContext);
            Broadcasts = new IncrementalObservableCollection<BroadcastProgramMiniViewModel>();

            ModelHelper.ConnectTo(Broadcasts, _nicoFavLive, w => w.LiveBroadcasts, w => new BroadcastProgramMiniViewModel(w));
        }
    }
}