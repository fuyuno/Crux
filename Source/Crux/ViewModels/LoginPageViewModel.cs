using System;
using System.Reactive.Linq;

using Crux.Mvvm;
using Crux.Services.Interfaces;

using Prism.Windows.Navigation;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Crux.ViewModels
{
    public class LoginPageViewModel : ViewModel
    {
        public ReactiveProperty<string> MailAddress { get; }
        public ReactiveProperty<string> Password { get; }
        public ReactiveCommand LoginCommand { get; }

        public LoginPageViewModel(IAccountService accountService, INavigationService navigationService)
        {
            MailAddress = new ReactiveProperty<string>().AddTo(this);
            Password = new ReactiveProperty<string>().AddTo(this);
            LoginCommand = new[]
            {
                MailAddress.Select(string.IsNullOrWhiteSpace),
                Password.Select(string.IsNullOrWhiteSpace)
            }.CombineLatestValuesAreAllFalse().ToReactiveCommand().AddTo(this);
            LoginCommand.Subscribe(async w =>
            {
                IsProcessing = true;
                await accountService.LoginAsync(MailAddress.Value, Password.Value);
                IsProcessing = false;
                if (!accountService.IsLoggedIn)
                    IsLoginFailure = true;
                else
                    navigationService.Navigate("Main", null);
            }).AddTo(this);
        }

        #region IsLoginFailure

        private bool _isLoginFailure;

        public bool IsLoginFailure
        {
            get { return _isLoginFailure; }
            set { SetProperty(ref _isLoginFailure, value); }
        }

        #endregion

        #region IsProcessing

        private bool _isProcessing;

        public bool IsProcessing
        {
            get { return _isProcessing; }
            set { SetProperty(ref _isProcessing, value); }
        }

        #endregion
    }
}