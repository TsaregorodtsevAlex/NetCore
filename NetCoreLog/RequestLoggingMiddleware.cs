using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Serilog.Context;
using Microsoft.AspNetCore.Http;

namespace NetCoreLog
{
	public class RequestLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _none = "-";

		public RequestLoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var httpRequest = context.Request;

			var pathBase = httpRequest.PathBase;
			var path = httpRequest.Path;
			var query = httpRequest.Query;
			var queryString = httpRequest.QueryString.ToString();
			var method = httpRequest.Method;
			var headers = httpRequest.Headers.ToDictionary(v => v.Key, v => v.Value.ToString());
			var host = httpRequest.Host.ToString();
			var body = await MapRequestBodyToString(context);
			var formData = MapRequestFormToObject(httpRequest);

			using (LogContext.PushProperty("Request.PathBase", GetSpecialString(pathBase)))
			using (LogContext.PushProperty("Request.Path", GetSpecialString(path)))
			using (LogContext.PushProperty("Request.Query", query))
			using (LogContext.PushProperty("Request.QueryString", GetSpecialString(queryString)))
			using (LogContext.PushProperty("Request.Method", GetSpecialString(method)))
			using (LogContext.PushProperty("Request.Headers", headers))
			using (LogContext.PushProperty("Request.Host", host))
			using (LogContext.PushProperty("Request.Body", body))
			using (LogContext.PushProperty("Request.FormData", formData))
			{
				await _next.Invoke(context);
			}
		}

		private string GetSpecialString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return _none;
			}

			return value;
		}

		private async Task<string> MapRequestBodyToString(HttpContext httpContext)
		{
			if (httpContext.Request?.Method != "POST")
			{
				return _none;
			}

			if (httpContext.Request?.ContentType?.ToLower()?.Contains("multipart/form-data") == true)
			{
				return "[multipart/form-data]";
			}

			var bodyStream = httpContext.Request.Body;

			if (bodyStream == null || bodyStream.CanRead == false)
			{
				return _none;
			}

			var bodyString = await new StreamReader(bodyStream).ReadToEndAsync();

			httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(bodyString));

			return bodyString;
		}

		private Dictionary<string, string> MapRequestFormToObject(HttpRequest httpRequest)
		{
			if (httpRequest.ContentType?.ToLower()?.Contains("multipart/form-data") == false || httpRequest.Method != "POST")
			{
				return null;
			}

			var formDictionary = httpRequest?.Form?.ToDictionary(v => v.Key, v => v.Value.ToString());

			if (formDictionary == null || formDictionary.Count == 0)
			{
				return null;
			}

			return formDictionary;
		}
	}
}
