using CommunityToolkit.Mvvm.ComponentModel;
using CryptoViewer.Models;
using CryptoViewer.Utils;

namespace CryptoViewer.ViewModels;

public class CoinViewModel : ObservableObject
{
    private readonly Coin _coin;

    public string Id { get => _coin.Id; }
    public string Name { get => _coin.Name; }
    public string ImageUri { get => _coin.ImageUri; }
    public string Price { get => _coin.Price?.ToKmbFormat() ?? "NaN"; }
    public string Volume { get => _coin.Volume?.ToKmbFormat() ?? "NaN"; }
    public string CirculatingSupply { get => _coin.CirculatingSupply?.ToKmbFormat() ?? "NaN"; }
    public string MarketCap { get => _coin.MarketCap?.ToKmbFormat() ?? "NaN"; }
    public string TodayPriceChange { get => _coin.PriceChange?.Today?.ToKmbFormat() ?? "NaN"; }
    public string InWeekPriceChange { get => _coin.PriceChange?.InWeek?.ToKmbFormat() ?? "NaN"; }
    public string InMonthPriceChange { get => _coin.PriceChange?.InMonth?.ToKmbFormat() ?? "NaN"; }
    public string InYearPriceChange { get => _coin.PriceChange?.InYear?.ToKmbFormat() ?? "NaN"; }

    public CoinViewModel(Coin coin)
    {
        _coin = coin;
    }
}
