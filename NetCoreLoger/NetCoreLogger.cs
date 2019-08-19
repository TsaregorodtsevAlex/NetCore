using System.Reflection;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace NetCoreLoger
{
    public static class NetCoreLogger
    {
        public static LoggerConfiguration ConfigureLogger(IConfigurationRoot configuration)
        {
            var moduleName = configuration["SERILOG_MODULE_NAME"];
            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Destructure.With<JsonDestructuringPolicy>()
                .Enrich.WithProperty("version", Assembly.GetExecutingAssembly().GetName().Version)
                .Enrich.WithProperty("module", moduleName);
        }
    }
}
