using System;
using System.Reactive.Linq;

using Crux.Mvvm;
using Crux.Services.Interfaces;

namespace Crux.ViewModels.Controls
{
    public class CurrentUserViewModel : ViewModel
    {
        private readonly IAccountService _accountService;

        public CurrentUserViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            IconUrl = "http://placehold.jp/24/dddddd/999999/48x48.png?text=A";
            Name = "アカウント";
            Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
                      .Subscribe(w =>
                      {
                          if (!_accountService.IsLoggedIn)
                              return;
                          IconUrl = _accountService.CurrentUser.IconUrl;
                          Name = _accountService.CurrentUser.Name;
                      });
        }

        #region IconUrl

        private string _iconUrl;

        public string IconUrl
        {
            get { return _iconUrl; }
            set { SetProperty(ref _iconUrl, value); }
        }

        #endregion

        #region Name

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        #endregion
    }
}