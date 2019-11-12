using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreCQRS.Integration.Test.Model
{
	[Table("Points")]
	public class Point
	{
		public long Id { get; set; }
		public DateTimeOffset DateTimeOffset { get; set; }
		public double Value { get; set; }
		public string Comment { get; set; }
	}
}
