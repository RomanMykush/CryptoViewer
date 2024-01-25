using CryptoViewer.Dtos;
using CryptoViewer.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace CryptoViewer.Services.CoinProvider;

public class CoinProvider : ICoinProvider
{
    public readonly HttpClient _httpClient;

    public CoinProvider(HttpClient httpClient, IOptions<ApplicationOptions> appOptions)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(appOptions.Value.ApiPath);
    }

    public async Task<IEnumerable<SimpleCoinDto>> SearchCoin(string query)
    {
        var response = await _httpClient.GetAsync($"search?query={query}");
        string jsonRes = await response.Content.ReadAsStringAsync();

        var jArray = JObject.Parse(jsonRes)["coins"] as JArray;

        if (jArray == null || !jArray.HasValues)
            return new List<SimpleCoinDto>();

        var result = JsonConvert.DeserializeObject<SimpleCoinDto[]>(jArray.ToString());

        if (result == null)
            return new List<SimpleCoinDto>();

        return result;
    }

    public async Task<IEnumerable<CoinDto>> GetCoins(string referenceCurrency, ICollection<string> ids)
    {
        string idsQuery = string.Join(",", ids);
        string requestQuery = $"coins/markets?vs_currency={referenceCurrency}&ids={idsQuery}&per_page={ids.Count}&sparkline=false&price_change_percentage=24h,7d,30d,1y";

        return await FetchCoins(requestQuery);
    }

    public async Task<IEnumerable<CoinDto>> GetTopNCoins(string referenceCurrency, int num)
    {
        string requestQuery = $"coins/markets?vs_currency={referenceCurrency}&per_page={num}&sparkline=false&price_change_percentage=24h,7d,30d,1y";

        return await FetchCoins(requestQuery);
    }

    private async Task<IEnumerable<CoinDto>> FetchCoins(string requestQuery)
    {
        var response = await _httpClient.GetAsync(requestQuery);
        string jsonRes = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<CoinDto[]>(jsonRes);

        if (result == null)
            return new List<CoinDto>();

        var priceChanges = JsonConvert.DeserializeObject<PriceChange[]>(jsonRes);

        for (int i = 0; i < result.Length; i++)
            result[i].PriceChange = priceChanges![i];

        return result;
    }
}
