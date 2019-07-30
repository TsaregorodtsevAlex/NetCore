using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetCoreApiKey
{
    public class AccessOnlyByApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessOnlyByApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("client-api-key") == false)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Request headers not contains client-api-key.");
                return;
            }

            var clientApiKey = context.Request.Headers["client-api-key"].ToString();
            if (ApiKeyStore.ContainsApiKey(clientApiKey) == false)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Request headers contains not valid client-api-key.");
                return;
            }

            await _next.Invoke(context);
        }
    }
}