using Crux.Mvvm;
using Crux.Services.Interfaces;

namespace Crux.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        private readonly IAccountService _accountService;

        public MainPageViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
    }
}