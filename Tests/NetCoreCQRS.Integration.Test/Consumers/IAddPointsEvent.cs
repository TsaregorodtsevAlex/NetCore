using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRS.Integration.Test.Consumers
{
	public interface IAddPointsEvent
	{
		int Count { get; set; }
	}
}
