using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetCoreHttpClient
{
    public class NetCoreHttpClient
    {
        private const string HttpClientMediaType = "application/json";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        private string _httpClientBaseAddress;

        public NetCoreHttpClient(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = new HttpClient();
        }

        public void ConfigureServiceHttpClient(NetCoreHttpClientConfigurationOptions configurationOptions)
        {
            _httpClientBaseAddress = configurationOptions.HttpClientBaseAddress;
        }

        public async Task<HttpClient> GetHttpClient()
        {
            _httpClient.BaseAddress = new Uri(_httpClientBaseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpClientMediaType));

            return _httpClient;
        }
    }
}
