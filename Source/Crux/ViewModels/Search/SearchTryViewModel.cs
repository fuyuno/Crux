using Crux.Services.Interfaces;

using Mntone.Nico2.Live.OnAirStreams;

using Prism.Windows.Navigation;

namespace Crux.ViewModels.Search
{
    public class SearchTryViewModel : SearchBaseViewModel
    {
        public SearchTryViewModel(IAccountService accountService, INavigationService navigationService)
            : base(accountService, navigationService, Category.Challenge) {}
    }
}