using Crux.Collections;
using Crux.Helpers;
using Crux.Models;
using Crux.Mvvm;
using Crux.Services.Interfaces;

using Prism.Windows.Navigation;

namespace Crux.ViewModels.Controls
{
    public class FavoriteBroadcastsViewModel : ViewModel
    {
        private readonly NicoFavLive _nicoFavLive;
        public IncrementalObservableCollection<BroadcastProgramMiniViewModel> Broadcasts { get; }

        public FavoriteBroadcastsViewModel(IAccountService accountService, INavigationService navigationService)
        {
            _nicoFavLive = new NicoFavLive(accountService.CurrentContext);
            Broadcasts = new IncrementalObservableCollection<BroadcastProgramMiniViewModel>();

            ModelHelper.ConnectTo(Broadcasts, _nicoFavLive, w => w.LiveBroadcasts,
                                  w => new BroadcastProgramMiniViewModel(navigationService, w));
        }
    }
}