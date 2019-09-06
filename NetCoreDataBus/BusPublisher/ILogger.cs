using System;

namespace NetCoreDataBus.BusPublisher
{
	public interface ILogger
	{
		void Information(string messageTemplate, params object[] propertyValues);
		void Error(Exception exception, string messageTemplate, params object[] propertyValues);
	}
}
