using AutoMapper;
using CryptoViewer.Models;
using CryptoViewer.Services.CoinProvider;
using CryptoViewer.Stores.CoinStore;
using CryptoViewer.Stores.ReferenceCurrencyStore;
using CryptoViewer.Utils;

namespace CryptoViewer.Services.CoinService;

public class CoinService : ICoinService
{
    private readonly ICoinProvider _coinProvider;
    private readonly CoinStore _coinStore;
    private readonly ReferenceCurrencyStore _referenceCurrencyStore;
    private readonly IMapper _mapper;

    public CoinService(ICoinProvider coinProvider, CoinStore coinStore, ReferenceCurrencyStore referenceCurrencyStore, IMapper mapper)
    {
        _coinProvider = coinProvider;
        _coinStore = coinStore;
        _referenceCurrencyStore = referenceCurrencyStore;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Coin>> GetTopNCoins(int num)
    {
        // Count check
        if (_coinStore.TopCoins.Count < num)
            return await FetchTopNCoins(num);

        // Check if is fresh
        if (_coinStore.TopCoins.IsExpired())
            return await FetchTopNCoins(num);

        IEnumerable<Coin?> coins = _coinStore.TopCoins.CoinsIds.Select(id => _coinStore.GetCoinById(id));

        // Check if data exist
        if (coins == null || coins.Any(c => c == null))
            return await FetchCoins(_coinStore.TopCoins.CoinsIds);

        return coins!;
    }

    private async Task<IEnumerable<Coin>> FetchTopNCoins(int num)
    {
        var coinDtos = await _coinProvider.GetTopNCoins(_referenceCurrencyStore.CurrentRefCurrency, num);
        var coins = _mapper.Map<List<Coin>>(coinDtos);

        coins.ForEach(c => _coinStore.AddOrUpdateCoin(c));
        _coinStore.SetTopCoins(coins.Select(c => c.Id));

        return coins;
    }

    public async Task<IEnumerable<Coin>> GetCoins(IEnumerable<string> ids)
    {
        IEnumerable<Coin?> coins = ids.Select(id => _coinStore.GetCoinById(id));

        // Check if data exist
        if (coins == null || coins.Any(c => c == null))
            return await FetchCoins(ids);
        // Check if it is full
        if (coins.Any(c => c!.IsIncomplete()))
            return await FetchCoins(ids);
        // Check if is fresh
        if (coins!.AreExpired())
            return await FetchCoins(ids);

        return coins!;
    }

    private async Task<IEnumerable<Coin>> FetchCoins(IEnumerable<string> ids)
    {
        var coinDtos = await _coinProvider.GetCoins(_referenceCurrencyStore.CurrentRefCurrency, ids.ToList());
        var coins = _mapper.Map<List<Coin>>(coinDtos);

        coins.ForEach(c => _coinStore.AddOrUpdateCoin(c));

        return coins;
    }

    public async Task<IEnumerable<Coin>> SearchCoin(string query)
    {
        var coinDtos = await _coinProvider.SearchCoin(query);
        var coins = _mapper.Map<List<Coin>>(coinDtos);

        coins.ForEach(c => _coinStore.AddOrUpdateCoin(c));

        return coins;
    }
}
