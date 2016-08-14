using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Mntone.Nico2;
using Mntone.Nico2.Live;

using Newtonsoft.Json;

namespace Crux.Extensions
{
    // Mntone.Nico2.Live.LiveApi の拡張
    public static class LiveApiExt
    {
        /// <summary>
        ///     参加しているコミュニティの中で、現在放送中の生放送の一覧を取得します。
        /// </summary>
        /// <returns></returns>
        public static async Task<Bookmark> BookmarkAsync(this LiveApi obj, int page = 1)
        {
            var field = obj.GetType().GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
            var context = (NiconicoContext) field.GetValue(obj);

            var method = context.GetType().GetMethod("GetClient");
            var httpClient = (HttpClient) method.Invoke(obj, null);
            var result = await httpClient.GetStringAsync($"http://live.nicovideo.jp/api/bookmark/json?type=onair&page={page}&number=6");
            return JsonConvert.DeserializeObject<Bookmark>(result);
        }
    }
}