namespace NetCoreCQRS.BaseResponses
{
    public class PagedListResponse<TItem> 
    {
        public TItem[] Items { get; set; }
        public int TotalCount { get; protected set; }
    }
}