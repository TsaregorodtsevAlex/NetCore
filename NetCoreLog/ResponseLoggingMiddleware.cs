using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace NetCoreLog
{
	public class ResponseLoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly string _none = "-";
		private readonly int _responseBodyStringMaxLength = 1500;

		public ResponseLoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var httpResponse = context.Response;
			var sourceBody = httpResponse?.Body;

			try
			{
				using (var ms = new MemoryStream())
				{
					httpResponse.Body = ms;

					await _next.Invoke(context);

					var responseBodyString = await MapStreamToString(ms);
					var transformedResponseBodyString = TransformResponseBody(context.Response, responseBodyString);

					ms.Position = 0;
					await ms.CopyToAsync(sourceBody);

					if (transformedResponseBodyString.Length > 0)
					{
						var headers = httpResponse.Headers.ToDictionary(v => v.Key, v => v.Value.ToString());
						var statusCode = httpResponse.StatusCode;

						Log.Information($"Response status code: {statusCode}");
						Log.Information($"Response headers: {JsonConvert.SerializeObject(headers)}");
						Log.Information($"Response body: {transformedResponseBodyString}");
					}
				}
			}
			finally
			{
				httpResponse.Body = sourceBody;
			}
		}

		private async Task<string> MapRequestBodyToString(HttpContext httpContext)
		{
			if (httpContext.Request?.Method != "POST")
			{
				return _none;
			}

			var bodyStream = httpContext.Request?.Body;

			if (bodyStream == null || bodyStream.CanRead == false)
			{
				return _none;
			}

			var ms = new MemoryStream();
			bodyStream.CopyTo(ms);

			if (ms?.Length == 0)
			{
				return _none;
			}

			ms.Position = 0;
			return await new StreamReader(ms).ReadToEndAsync();
		}

		private string TransformResponseBody(HttpResponse httpResponse, string responseBodyString)
		{
			if (httpResponse.Headers["content-disposition"].Count > 0)
			{
				var isContainsFile = httpResponse.Headers["content-disposition"][0]?.Contains("filename");
				if (isContainsFile == true)
				{
					return "multipart/form-data";
				}
			}
			return TruncateResponseString(responseBodyString);
		}

		private string TruncateResponseString(string reponseBodyString)
		{
			var bodyLength = reponseBodyString.Length;

			if (bodyLength > _responseBodyStringMaxLength)
			{
				bodyLength = _responseBodyStringMaxLength;
				return $"{reponseBodyString.Substring(0, bodyLength)}...and so on; the response's body was truncated because it is too long }}";
			}

			return reponseBodyString;
		}

		private async Task<string> MapStreamToString(MemoryStream ms)
		{
			if (ms == null)
			{
				return _none;
			}

			ms.Position = 0;
			return await new StreamReader(ms).ReadToEndAsync();
		}
	}
}
