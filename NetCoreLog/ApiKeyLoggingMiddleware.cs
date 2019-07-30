using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace NetCoreLog
{
    public class ApiKeyLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientApiKey = context.Request.Headers.ContainsKey("client-api-key") ? context.Request.Headers["client-api-key"].ToString() : "no api key";
            var clientApiName = context.Request.Headers.ContainsKey("client-api-name") ? context.Request.Headers["client-api-name"].ToString() : "no api name";
            var clientApiRequestId = context.Request.Headers.ContainsKey("client-api-request-id") ? context.Request.Headers["client-api-name"].ToString() : "no request id";

            using (LogContext.PushProperty("client-api-key", clientApiKey))
            using (LogContext.PushProperty("client-api-name", clientApiName))
            using (LogContext.PushProperty("client-api-request-id", clientApiRequestId))
            {
                await _next.Invoke(context);
            }
        }
    }
}