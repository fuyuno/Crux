using System;
using System.Collections.Generic;
using System.Reactive.Linq;

using Windows.Media.Core;

using Crux.Models;
using Crux.Mvvm;
using Crux.Services.Interfaces;

using Prism.Windows.Navigation;

using Reactive.Bindings.Extensions;

namespace Crux.ViewModels
{
    public class LivePageViewModel : ViewModel
    {
        private readonly IAccountService _accountService;
        private NicoLiveView _nicoLiveView;

        public LivePageViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            CommunityImage = "http://placehold.jp/333333/ffffff/96x96.png?text=No%20Image";
        }

        #region Overrides of ViewModelBase

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);
            _nicoLiveView = new NicoLiveView(_accountService, e.Parameter.ToString());
            _nicoLiveView.Start();
            _nicoLiveView.ObserveProperty(w => w.PlayerStatus).Where(w => w != null)
                         .ObserveOnUIDispatcher()
                         .Subscribe(w =>
                         {
                             Title = w.Program.Title;
                             CommunityImage = w.Program.CommunityImageUrl.ToString();
                         })
                         .AddTo(this);
            _nicoLiveView.ObserveProperty(w => w.MediaStreamSource).Where(w => w != null)
                         .ObserveOnUIDispatcher()
                         .Subscribe(w => MediaStreamSource = w)
                         .AddTo(this);
        }

        #endregion

        #region Overrides of ViewModel

        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            _nicoLiveView.Finish();
            base.OnNavigatingFrom(e, viewModelState, suspending);
        }

        #endregion

        #region Title

        private string _title;

        public string Title
        {
            get { return _title; }
            private set { SetProperty(ref _title, value); }
        }

        #endregion

        #region CommunityImage

        private string _communityImage;

        public string CommunityImage
        {
            get { return _communityImage; }
            private set { SetProperty(ref _communityImage, value); }
        }

        #endregion

        #region MediaStreamSource

        private IMediaSource _mediaStreamSource;

        public IMediaSource MediaStreamSource
        {
            get { return _mediaStreamSource; }
            set { SetProperty(ref _mediaStreamSource, value); }
        }

        #endregion

        #region IsLiving

        private bool _isLiving;

        public bool IsLiving
        {
            get { return _isLiving; }
            set
            {
                if (SetProperty(ref _isLiving, value) && !value)
                    _nicoLiveView.Finish();
            }
        }

        #endregion
    }
}