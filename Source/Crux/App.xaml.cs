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

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        #region Overrides of PrismApplication

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            UIDispatcherScheduler.Initialize();

            Container.RegisterType<IAccountService, AccountService>(new ContainerControlledLifetimeManager());

            return base.OnInitializeAsync(args);
        }

        #endregion

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            // NavigationService.Navigate("HomeMain", null);
            return Task.CompletedTask;
        }

        #endregion
    }
}