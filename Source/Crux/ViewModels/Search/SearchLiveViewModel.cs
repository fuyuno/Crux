using Crux.Services.Interfaces;

using Mntone.Nico2.Live.OnAirStreams;

using Prism.Windows.Navigation;

namespace Crux.ViewModels.Search
{
    public class SearchLiveViewModel : SearchBaseViewModel
    {
        public SearchLiveViewModel(IAccountService accountService, INavigationService navigationService)
            : base(accountService, navigationService, Category.Game) {}
    }
}