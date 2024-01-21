using CryptoViewer.Models;

namespace CryptoViewer.Stores;

public class CoinStore
{
    private Dictionary<string, Coin> CoinsByName = new();
    private Dictionary<string, Coin> CoinsBySymbol = new();

    public bool AreExpired(ICollection<Coin> coins) => coins.Any(c => c.IsExpired());

    public Coin? GetCoinByName(string name)
    {
        if (!CoinsByName.ContainsKey(name))
            return null;
        return CoinsByName[name];
    }

    public Coin? GetCoinBySymbol(string symbol)
    {
        if (!CoinsBySymbol.ContainsKey(symbol))
            return null;
        return CoinsBySymbol[symbol];
    }

    public void AddOrUpdateCoin(Coin coin)
    {
        CoinsByName[coin.Name] = coin;
        CoinsByName[coin.Symbol] = coin;
    }

    public void RemoveCoin(Coin coin)
    {
        if (CoinsByName.ContainsKey(coin.Name))
            CoinsByName.Remove(coin.Name);
        if (CoinsBySymbol.ContainsKey(coin.Symbol))
            CoinsBySymbol.Remove(coin.Symbol);
    }
}
