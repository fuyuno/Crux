using Crux.Services.Interfaces;

using Mntone.Nico2.Live.OnAirStreams;

using Prism.Windows.Navigation;

namespace Crux.ViewModels.Search
{
    public class SearchFaceViewModel : SearchBaseViewModel
    {
        public SearchFaceViewModel(IAccountService accountService, INavigationService navigationService)
            : base(accountService, navigationService, Category.FaceOut) {}
    }
}