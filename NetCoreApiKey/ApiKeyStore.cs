using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreApiKey
{
    public static class ApiKeyStore
    {
        private static Dictionary<string, string> _apiClients;

        public static void ConfigureStore(string apiKeys)
        {
            var apiKeyNamePairs = apiKeys.Split(';');
            _apiClients = apiKeyNamePairs.Select(p => p.Split('#')).ToDictionary(p => p[0], p => p[1]);
        }

        public static bool ContainsApiKey(string apiKey)
        {
            return _apiClients.ContainsKey(apiKey);
        }
    }
}
