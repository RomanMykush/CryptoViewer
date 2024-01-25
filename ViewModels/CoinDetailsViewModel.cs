using CommunityToolkit.Mvvm.ComponentModel;
using CryptoViewer.Models;
using CryptoViewer.Services.CoinService;
using CryptoViewer.Utils;

namespace CryptoViewer.ViewModels;

public partial class CoinDetailsViewModel : ObservableRecipient, IPageViewModel
{
    public required string CoinId { get; set; }

    private readonly ICoinService _coinService;
    private Coin? _coin;

    [ObservableProperty]
    private DataState _currentDataState = DataState.Loading;

    public string Name { get => _coin?.Name ?? "NaN"; }
    public string ImageUri { get => _coin?.ImageUri ?? string.Empty; }
    public string Price { get => _coin?.Price?.ToKmbFormat() ?? "NaN"; }
    public string Volume { get => _coin?.Volume?.ToKmbFormat() ?? "NaN"; }
    public string CirculatingSupply { get => _coin?.CirculatingSupply?.ToKmbFormat() ?? "NaN"; }
    public string MarketCap { get => _coin?.MarketCap?.ToKmbFormat() ?? "NaN"; }
    public string TodayPriceChange { get => _coin?.PriceChange?.Today?.ToKmbFormat() ?? "NaN"; }
    public string InWeekPriceChange { get => _coin?.PriceChange?.InWeek?.ToKmbFormat() ?? "NaN"; }
    public string InMonthPriceChange { get => _coin?.PriceChange?.InMonth?.ToKmbFormat() ?? "NaN"; }
    public string InYearPriceChange { get => _coin?.PriceChange?.InYear?.ToKmbFormat() ?? "NaN"; }

    public CoinDetailsViewModel(ICoinService coinService)
    {
        _coinService = coinService;
        _ = LoadData();
    }

    private async Task LoadData()
    {
        List<string> list = [CoinId];
        var coins = await _coinService.GetCoins(list);
        _coin = coins.First();
        OnPropertyChanged(string.Empty);

        CurrentDataState = DataState.Available;
    }
}
