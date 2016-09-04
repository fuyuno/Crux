using System.Collections.Generic;

using Crux.Mvvm;
using Crux.Services.Interfaces;

using Prism.Windows.Navigation;

namespace Crux.ViewModels
{
    public class LivePageViewModel : ViewModel
    {
        private readonly IAccountService _accountService;

        public string Title { get; private set; }

        public LivePageViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        #region Overrides of ViewModelBase

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion
    }
}