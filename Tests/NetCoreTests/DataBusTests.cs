using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NetCoreDataBus;
using NUnit.Framework;

namespace NetCoreTests
{
    [TestFixture]
    public class DataBusTests: BaseTest
    {
        [Test]
        public void Test()
        {
            //var bus = ServiceProvider.GetService<IBus>();

            //bus.Publish(new YourMessage { Text = "Hi" });
        }
    }
}