using System.Configuration;

namespace NetCoreHttpClient
{
    public class NetCoreHttpClientConfigurationOptions: ConfigurationSection
    {
        [ConfigurationProperty("httpClientBaseAddress", IsRequired = true)]
        public string HttpClientBaseAddress
        {
            get => (string) this["httpClientBaseAddress"];
            set => this["httpClientBaseAddress"] = value;
        }
    }
}