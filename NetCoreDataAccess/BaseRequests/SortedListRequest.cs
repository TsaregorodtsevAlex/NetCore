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
				if (string.IsNullOrWhiteSpace(FieldName))
				{
					return string.Empty;
				}

				var direction = Direction == SortDirection.Descending ? "DESC" : string.Empty;
				return $"{FieldName} {direction}".Trim();
			}
		}
	}
}