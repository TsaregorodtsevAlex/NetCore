using System;
using System.Threading.Tasks;
using Serilog.Context;
using Microsoft.AspNetCore.Http;

namespace NetCoreLoger
{
	public class ProcessIdLoggingMiddleware
	{
		private readonly RequestDelegate _next;

		public ProcessIdLoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			string processId = null;
			if (context.Request.Headers.ContainsKey("processId") == false)
			{
				processId = Guid.NewGuid().ToString();
				context.Request.Headers.Add("processId", processId);
			}
			else
			{
				processId = context.Request.Headers["processId"].ToString();
			}

			using (LogContext.PushProperty("processId", processId))
			{
				await _next.Invoke(context);
			}
		}
	}
}
