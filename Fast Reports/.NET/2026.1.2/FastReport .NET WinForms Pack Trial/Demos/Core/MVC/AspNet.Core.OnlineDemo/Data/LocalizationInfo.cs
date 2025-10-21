using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace AspNet.Demo.Data;

internal static class LocalizationInfo
{
    public static RequestLocalizationOptions GetRequestLocalizationOptions(Dictionary<string, string> localizationInfoOptions)
    {
        var cultures = localizationInfoOptions.Keys.Select(key => new CultureInfo(key)).ToList();
        return new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture(cultures.First()),
            ApplyCurrentCultureToResponseHeaders = true,
            RequestCultureProviders = [new HostRequestCultureProvider(localizationInfoOptions)],
            SupportedCultures = cultures,
            SupportedUICultures = cultures,
        };
    }


    private sealed class HostRequestCultureProvider : IRequestCultureProvider
    {
        private readonly Dictionary<string, ProviderCultureResult> CultureResults;

        public HostRequestCultureProvider(Dictionary<string, string> localizationInfoOptions)
        {
            CultureResults = localizationInfoOptions
                .Select(kv => (kv.Value, new ProviderCultureResult(kv.Key)))
                .ToDictionary();
        }


        public Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
            => Task.FromResult(DetermineProviderCultureResult(httpContext.Request));


        private ProviderCultureResult? DetermineProviderCultureResult(HttpRequest httpRequest)
        {
            CultureResults.TryGetValue(httpRequest.Host.Value, out var result);
            return result;
        }
    }
}
