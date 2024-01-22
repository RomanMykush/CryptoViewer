using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;

namespace CryptoViewer.Services.ReferenceCurrencyProvider;

public class ReferenceCurrencyProvider : IReferenceCurrencyProvider
{
    public readonly HttpClient _httpClient;

    public ReferenceCurrencyProvider(HttpClient httpClient, IOptions<ApplicationOptions> appOptions)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(appOptions.Value.ApiPath);
    }

    public async Task<IEnumerable<string>> GetReferenceCurrencies()
    {
        var response = await _httpClient.GetAsync("simple/supported_vs_currencies");
        var jsonRes = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<string[]>(jsonRes);

        if (result == null)
            return new List<string>();

        return result;
    }
}
