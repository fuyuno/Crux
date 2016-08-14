using System.Collections.Generic;

using Newtonsoft.Json;

namespace Crux.Extensions
{
    public class Bookmark
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("bookmarkStreams")]
        public List<BookmarkStream> BookmarkStreams { get; set; }
    }
}