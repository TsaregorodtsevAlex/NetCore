namespace NetCoreCQRS.BaseRequests
{
    public class PagedListRequest : SortedListRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}