using System.Diagnostics;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace NetCoreLog
{
	public class ElapsedTimeLoggingMiddleware
	{
		private readonly RequestDelegate _next;

		public ElapsedTimeLoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var timer = Stopwatch.StartNew();

			await _next.Invoke(context);

			Log.Information("Method's execution time: {ElapsedTime: 0.00} ms", timer.ElapsedMilliseconds);
		}
	}
}
