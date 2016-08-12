using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Microsoft.Practices.Unity;

using Prism.Unity.Windows;

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
            return shell;
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            // NavigationService.Navigate("HomeMain", null);
            return Task.CompletedTask;
        }

        #endregion
    }
}