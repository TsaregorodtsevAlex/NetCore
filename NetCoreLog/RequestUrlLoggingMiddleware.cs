using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace NetCoreLog
{
    public class RequestUrlLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestUrlLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (LogContext.PushProperty("url-host", context.Request.Host))
            using (LogContext.PushProperty("url-path", context.Request.Path))
            {
                await _next.Invoke(context);
            }
        }
    }
}