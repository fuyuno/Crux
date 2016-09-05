using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Windows.Media.Core;

using Crux.Helpers;
using Crux.Services.Interfaces;

using Mntone.Nico2.Live.PlayerStatus;

using Prism.Mvvm;

namespace Crux.Models
{
    internal class NicoLivePlayer : BindableBase
    {
        private readonly IAccountService _accountService;
        private readonly string _liveId;
        private IDisposable _disposable;

        public NicoLivePlayer(IAccountService accountService, string liveId)
        {
            _accountService = accountService;
            _liveId = liveId;
        }

        public void Start() => RunHelper.RunAsync(StartAsync);

        private async Task StartAsync()
        {
            PlayerStatus = await _accountService.CurrentContext.Live.GetPlayerStatusAsync(_liveId);
            if (PlayerStatus.Stream.RtmpUrl == null)
                return; // 入れない。
            // rtmpdump -vr rtmp://{RtmpUrl} -N {StramContents} -C S:{RtmpTicket}
            _disposable = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMinutes(1))
                                    .Subscribe(async w =>
                                    {
                                        await _accountService.CurrentContext.Live.HeartbeatAsync(_liveId);
                                        Debug.WriteLine("heartbeat");
                                    });
        }

        public void Finish() => RunHelper.RunAsync(FinishAsync);

        private async Task FinishAsync()
        {
            if (_disposable == null)
                return;
            await _accountService.CurrentContext.Live.LeaveAsync(_liveId);
            Debug.WriteLine("leave");
            _disposable.Dispose();
        }

        #region PlayerStatus

        private PlayerStatusResponse _playerStatus;

        public PlayerStatusResponse PlayerStatus
        {
            get { return _playerStatus; }
            private set { SetProperty(ref _playerStatus, value); }
        }

        #endregion

        #region MediaStreamSource

        private IMediaSource _mediaStreamSource;

        public IMediaSource MediaStreamSource
        {
            get { return _mediaStreamSource; }
            private set { SetProperty(ref _mediaStreamSource, value); }
        }

        #endregion
    }
}