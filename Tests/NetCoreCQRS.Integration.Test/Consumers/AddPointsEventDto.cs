namespace NetCoreCQRS.Integration.Test.Consumers
{
	public class AddPointsEventDto : IAddPointsEvent
	{
		public int Count { get; set; }
	}
}
