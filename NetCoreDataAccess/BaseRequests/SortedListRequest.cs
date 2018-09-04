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
                var direction = Direction == SortDirection.Descending ? " DESC" : string.Empty;

                if(string.IsNullOrEmpty(FieldName) || string.IsNullOrEmpty(FieldName.Trim()))
                {
                    return string.Empty;
                }

                return $"{FieldName}{direction}";
            }
        }
    }
}