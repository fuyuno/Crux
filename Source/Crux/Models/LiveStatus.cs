using System;

namespace Crux.Models
{
    public enum LiveStatus
    {
        Connecting,

        Connected,

        Disconnected
    }

    public static class LiveStatusExt
    {
        public static string GetDisplayString(this LiveStatus obj)
        {
            switch (obj)
            {
                case LiveStatus.Connecting:
                    return "接続中";

                case LiveStatus.Connected:
                    return "接続完了";

                case LiveStatus.Disconnected:
                    return "切断";

                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
    }
}