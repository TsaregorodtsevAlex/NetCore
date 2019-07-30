using Serilog.Core;
using Serilog.Events;

namespace NetCoreLoger
{
    public class JsonDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            result = new JsonLogEventPropertyValue(value);
            return true;
        }
    }
}