namespace NetCoreCQRS.BaseResponses
{
    public class PagedListResponse
    {
        public int DisplayFrom { get; set; }
        public int DisplayTo { get; set; }
    }

    public class PagedListResponse<TItem> : PagedListResponse
    {
        public TItem[] Items { get; set; }
        public int SelectedCount { get; protected set; }
    }
}