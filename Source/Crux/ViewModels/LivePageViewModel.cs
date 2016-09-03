using System.Collections.Generic;
using System.Diagnostics;

using Crux.Mvvm;

using Prism.Windows.Navigation;

namespace Crux.ViewModels
{
    public class LivePageViewModel : ViewModel
    {
        #region Overrides of ViewModelBase

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            Debug.WriteLine(e.Parameter);
        }

        #endregion
    }
}