using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreDataAccess.BaseRequests
{
    public class PagedListRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public List<SortedListRequest> Sortings { get; set; }

        public bool HasSortings => Sortings != null && Sortings.Any();

        public string SortingAsSqlQueryString => HasSortings ? string.Join(",", Sortings.Select(s => s.AsSqlQueryString).ToArray()) : string.Empty;
    }
}