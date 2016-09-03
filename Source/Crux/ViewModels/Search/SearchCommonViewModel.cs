using Crux.Services.Interfaces;

using Mntone.Nico2.Live.OnAirStreams;

using Prism.Windows.Navigation;

namespace Crux.ViewModels.Search
{
    public class SearchCommonViewModel : SearchBaseViewModel
    {
        public SearchCommonViewModel(IAccountService accountService, INavigationService navigationService)
            : base(accountService, navigationService, Category.General) {}
    }
}