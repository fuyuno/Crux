using Newtonsoft.Json;

namespace Crux.Extensions
{
    public class CommunityInfo
    {
        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }
}