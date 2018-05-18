using System;
using Microsoft.AspNetCore.Http;

namespace NetCoreLocalization
{
    public class LocalizationService: ILocalizationService
    {
        private const string DefaultLanguageCode = "ru";
        private const string LanguageCookieKey = "system_language";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocalizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CurrentLanguageCode => _httpContextAccessor.HttpContext.Request.Cookies.Keys.Contains(LanguageCookieKey) ? _httpContextAccessor.HttpContext.Request.Cookies[LanguageCookieKey] : DefaultLanguageCode;
    }
}
