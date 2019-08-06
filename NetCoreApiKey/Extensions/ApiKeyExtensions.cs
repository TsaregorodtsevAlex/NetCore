using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreApiKey.Extensions
{
	public static class ApiKeyExtensions
	{
		/// <summary>
		/// Read keys from ApiKeyStore -> Keys
		/// </summary>
		public static IServiceCollection ConfigureApiKeyStore(this IServiceCollection collection, IConfiguration configuration)
		{
			var storeSection = configuration.GetSection("ApiKeyStore");
			var apiKeys = storeSection.GetValue<string>("Keys");
			ApiKeyStore.ConfigureStore(apiKeys);
			return collection;
		}

		/// <summary>
		/// Use middleware for api key
		/// </summary>
		public static IApplicationBuilder UseAccessOnlyByApiKeyMiddleware(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<AccessOnlyByApiKeyMiddleware>();
			return builder;
		}
	}
}
