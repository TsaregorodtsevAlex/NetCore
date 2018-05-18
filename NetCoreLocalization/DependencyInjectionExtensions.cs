using Microsoft.Extensions.DependencyInjection;

namespace NetCoreLocalization
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddNetCoreLocalizationService(this IServiceCollection service)
        {
            service
                .AddTransient<ILocalizationService, LocalizationService>();

            return service;
        }
    }
}