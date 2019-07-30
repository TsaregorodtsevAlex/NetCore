namespace NetCoreDataAccess.BaseRequests
{
    public class SortedListRequest
    {
        public string FieldName { get; set; }

        public SortDirection Direction { get; set; }

        public string AsSqlQueryString
        {
            get
            {
                var direction = Direction == SortDirection.Descending ? "DESC" : string.Empty;
                return $"{FieldName} {direction}";
            }
        }
    }
}