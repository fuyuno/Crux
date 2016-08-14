using System;

using Newtonsoft.Json;

namespace Crux.Extensions
{
    public class BookmarkStream
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("_communityinfo")]
        public CommunityInfo CommunityInfo { get; set; }

        [JsonProperty("isTimeshift")]
        public bool IsTimeshift { get; set; }
    }
}