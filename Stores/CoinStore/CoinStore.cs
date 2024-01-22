using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.Models;
using CryptoViewer.Stores.ReferenceCurrencyStore;

namespace CryptoViewer.Stores.CoinStore;

public class CoinStore
{
    private Dictionary<string, Coin> CoinsById = [];
    private Dictionary<string, Coin> CoinsBySymbol = [];

    public CoinStore()
    {
        WeakReferenceMessenger.Default.Register<CoinStore, CurrentRefCurrencyChangedMessage>(this, (r, m) => Ckear());
    }

    public Coin? GetCoinById(string id)
    {
        if (!CoinsById.ContainsKey(id))
            return null;
        return CoinsById[id];
    }

    public Coin? GetCoinBySymbol(string symbol)
    {
        if (!CoinsBySymbol.ContainsKey(symbol))
            return null;
        return CoinsBySymbol[symbol];
    }

    public void AddOrUpdateCoin(Coin coin)
    {
        CoinsById[coin.Id] = coin;
        CoinsBySymbol[coin.Symbol] = coin;
        WeakReferenceMessenger.Default.Send(new CoinUpdatedMessage(coin));
    }

    public void RemoveCoin(Coin coin)
    {
        if (CoinsById.ContainsKey(coin.Id))
            CoinsById.Remove(coin.Id);
        if (CoinsBySymbol.ContainsKey(coin.Symbol))
            CoinsBySymbol.Remove(coin.Symbol);
    }

    public void Ckear()
    {
        CoinsById.Clear();
        CoinsBySymbol.Clear();
    }
}
