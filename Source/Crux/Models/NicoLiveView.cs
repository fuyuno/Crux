using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Windows.Media.Core;

using Crux.Helpers;
using Crux.Services.Interfaces;

using Mntone.Nico2.Live.PlayerStatus;
using Mntone.Rtmp.Client;

using Prism.Mvvm;

namespace Crux.Models
{
    internal class NicoLiveView : BindableBase
    {
        private readonly IAccountService _accountService;
        private readonly string _liveId;
        private readonly SimpleVideoClient _videoClient;
        private IDisposable _disposable;

        public NicoLiveView(IAccountService accountService, string liveId)
        {
            _accountService = accountService;
            _liveId = liveId;
            _videoClient = new SimpleVideoClient();
            _videoClient.Started += OnStarted;
            _videoClient.Stopped += OnStopped;
        }

        private void OnStarted(object sender, SimpleVideoClientStartedEventArgs e)
        {
            MediaStreamSource = e.MediaStreamSource;
        }

        private void OnStopped(object sender, SimpleVideoClientStoppedEventArgs e) => Finish();

        public void Start() => RunHelper.RunAsync(StartAsync);

        private async Task StartAsync()
        {
            PlayerStatus = await _accountService.CurrentContext.Live.GetPlayerStatusAsync(_liveId);
            if (PlayerStatus.Stream.RtmpUrl == null)
                return; // 入れない。
            // rtmpdump -vr rtmp://{RtmpUrl} -N {StramContents} -C S:{RtmpTicket}
            await _videoClient.ConnectAsync(PlayerStatus.Stream.RtmpUrl);
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