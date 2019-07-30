using System;
using System.IO;
using Newtonsoft.Json;
using Serilog.Events;

namespace NetCoreLoger
{
    public class JsonLogEventPropertyValue : LogEventPropertyValue
    {
        private readonly object _data;

        public JsonLogEventPropertyValue(object data)
        {
            _data = data;
        }

        public override void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
        {
            var serializer = JsonSerializer.Create(null);
            using (var jsonWriter = new JsonTextWriter(output))
            {
                jsonWriter.QuoteName = false;
                jsonWriter.StringEscapeHandling = StringEscapeHandling.Default;
                serializer.Serialize(jsonWriter, _data);
            }
        }
    }
}