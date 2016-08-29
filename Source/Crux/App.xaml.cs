using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Crux.Services;
using Crux.Services.Interfaces;

using Microsoft.Practices.Unity;

using Prism.Unity.Windows;

using Reactive.Bindings;

namespace Crux
{
    /// <summary>
    ///     既定の Application クラスを補完するアプリケーション固有の動作を提供します。
    /// </summary>
    public sealed partial class App : PrismUnityApplication
    {
        public App()
        {
            InitializeComponent();
        }

        #region Overrides of PrismApplication

        #region Overrides of PrismApplication

        protected override async Task OnSuspendingApplicationAsync()
        {
            var accountService = Container.Resolve<IAccountService>();
            await accountService.LogoutAsync(false);
            await base.OnSuspendingApplicationAsync();
        }

        #endregion

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        protected override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            UIDispatcherScheduler.Initialize();

            var accountService = new AccountService();
            Container.RegisterInstance<IAccountService>(accountService, new ContainerControlledLifetimeManager());

            await accountService.LoginAsync();
            await base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            var accountService = Container.Resolve<IAccountService>();
            NavigationService.Navigate(accountService.IsLoggedIn ? "Main" : "Login", null);
            return Task.CompletedTask;
        }

        #endregion
    }
}