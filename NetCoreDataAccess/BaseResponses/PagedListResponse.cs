namespace NetCoreDataAccess.BaseResponses
{
    public class PagedListResponse<TItem> : ListResponseBase
    {
        public TItem[] Items { get; set; }
    }
}