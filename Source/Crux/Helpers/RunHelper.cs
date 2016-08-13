using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Reactive.Bindings.Extensions;

namespace Crux.Helpers
{
    internal class RunHelper
    {
        /// <summary>
        ///     バックグラウンドスレッド上で、 action を実行します。
        /// </summary>
        /// <param name="action"></param>
        public static void Run(Action action)
        {
            Task.Run(action);
        }

        /// <summary>
        ///     バックグラウンドスレッド上で、非同期操作 action を実行します。
        /// </summary>
        /// <param name="action"></param>
        public static void RunAsync(Func<Task> action)
        {
            Task.Run(async () => await action.Invoke());
        }

        /// <summary>
        ///     UI スレッド上で、 action を dueTime 後に実行します。
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dueTime"></param>
        public static void RunLater(Action action, TimeSpan dueTime)
        {
            Observable.Return(0).Delay(dueTime).ObserveOnUIDispatcher().Subscribe(w => action.Invoke());
        }

        /// <summary>
        ///     UI スレッド上で、 非同期操作 action を dueTime 後に実行します。
        /// </summary>
        /// <param name="action"></param>
        /// <param name="dueTime"></param>
        public static void RunLaterAsync(Func<Task> action, TimeSpan dueTime)
        {
            Observable.Return(0).Delay(dueTime).ObserveOnUIDispatcher().Subscribe(async w => await action.Invoke());
        }
    }
}