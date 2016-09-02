using System.Collections.Generic;
using System.Collections.ObjectModel;

using Mntone.Nico2;
using Mntone.Nico2.Live.OnAirStreams;

namespace Crux.Models
{
    public class LiveSort
    {
        private SortType SortType { get; }
        public SortDirection SortDirection { get; }
        public string Name { get; }

        private LiveSort(SortType sortType, SortDirection sortDirection, string name)
        {
            SortType = sortType;
            SortDirection = sortDirection;
            Name = name;
        }

        #region Static Properties

        public static LiveSort StartTimeDesc => new LiveSort(SortType.StartTime, SortDirection.Descending, "放送日時が近い順");
        public static LiveSort StartTimeAsc => new LiveSort(SortType.StartTime, SortDirection.Ascending, "放送日時が遠い順");
        public static LiveSort ViewCountDesc => new LiveSort(SortType.ViewCount, SortDirection.Descending, "来場者数が多い順");
        public static LiveSort ViewCountAsc => new LiveSort(SortType.ViewCount, SortDirection.Ascending, "来場者数が少ない順");
        public static LiveSort CommentCountDesc => new LiveSort(SortType.CommentCount, SortDirection.Descending, "コメント数が多い順");
        public static LiveSort CommentCountAsc => new LiveSort(SortType.CommentCount, SortDirection.Ascending, "コメント数が少ない順");
        public static LiveSort CommunityLvDesc => new LiveSort(SortType.CommunityLevel, SortDirection.Descending, "コミュニティレベルが高い順");
        public static LiveSort CommunityLvAsc => new LiveSort(SortType.CommunityLevel, SortDirection.Ascending, "コミュニティレベルが低い順");
        public static LiveSort CommunityCreateDesc => new LiveSort(SortType.CommunityCreateTime, SortDirection.Descending, "コミュニティが新しい順");
        public static LiveSort CommunityCreateAsc => new LiveSort(SortType.CommunityCreateTime, SortDirection.Ascending, "コミュニティが古い順");

        public static ReadOnlyCollection<LiveSort> Sorts => new List<LiveSort>
        {
            StartTimeDesc,
            StartTimeAsc,
            ViewCountDesc,
            ViewCountAsc,
            CommentCountDesc,
            CommentCountAsc,
            CommunityLvDesc,
            CommunityLvAsc,
            CommunityCreateDesc,
            CommunityCreateAsc
        }.AsReadOnly();

        #endregion
    }
}