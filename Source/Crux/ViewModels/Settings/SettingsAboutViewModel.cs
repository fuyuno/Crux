using System;

using Crux.Mvvm;

using Microsoft.Services.Store.Engagement;

namespace Crux.ViewModels.Settings
{
    public class SettingsAboutViewModel : ViewModel
    {
        public async void OnClickFeedback()
        {
            var launcher = StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }
    }
}