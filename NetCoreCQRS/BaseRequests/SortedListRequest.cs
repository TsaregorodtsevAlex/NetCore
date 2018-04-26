namespace NetCoreCQRS.BaseRequests
{
    public class SortedListRequest
    {
        public virtual string FieldName { get; set; }

        public virtual SortDirection Direction { get; set; }
    }
}