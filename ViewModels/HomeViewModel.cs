using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoViewer.Models;
using CryptoViewer.Services.CoinService;
using CryptoViewer.Services.NavigationService;
using CryptoViewer.Stores.ReferenceCurrencyStore;
using CryptoViewer.Utils;
using Meziantou.Framework.WPF.Collections;
using System.Windows.Threading;

namespace CryptoViewer.ViewModels;

public partial class HomeViewModel : ObservableRecipient, IPageViewModel
{
    private readonly ICoinService _coinService;
    private readonly ReferenceCurrencyStore _referenceCurrencyStore;
    private readonly INavigationService _navigationService;
    const int TopCoinNumber = 10;

    public ConcurrentObservableCollection<CoinViewModel> Coins { get; set; } = [];
    public ConcurrentObservableCollection<string> ReferenceCurrencies { get; set; } = [];
    public string SelectedRefCurrency
    {
        get => _referenceCurrencyStore.CurrentRefCurrency;
        set
        {
            if (value != null)
                _referenceCurrencyStore.CurrentRefCurrency = value;
            OnPropertyChanged(nameof(SelectedRefCurrency));

            BeginDataLoading();
        }
    }

    [ObservableProperty]
    public string _searchQuery = string.Empty;

    public IEnumerable<string> SearchResults = [];

    private DispatcherTimer UpdateTimer = new() { Interval = TimeSpan.FromMinutes(1) };

    [ObservableProperty]
    private DataState _currentDataState = DataState.Loading;

    public HomeViewModel(ICoinService coinService, ReferenceCurrencyStore referenceCurrencyStore, INavigationService navigationService)
    {
        _navigationService = navigationService;
        _referenceCurrencyStore = referenceCurrencyStore;
        _coinService = coinService;

        UpdateTimer.Tick += UpdateTimer_Tick;
        _ = LoadData();
    }

    private async Task LoadData()
    {
        IEnumerable<Coin>? coins = null;
        IEnumerable<string>? referenceCurrencies = null;
        try
        {
            if (SearchResults.Any())
                coins = await _coinService.GetCoins(SearchResults);
            else if(string.IsNullOrEmpty(SearchQuery))
                coins = await _coinService.GetTopNCoins(TopCoinNumber);

            if (!ReferenceCurrencies.Any())
                referenceCurrencies = await _referenceCurrencyStore.GetReferenceCurrencies();
        }
        catch (Exception)
        {
            CurrentDataState = DataState.Failed;
            return;
        }
        finally { UpdateTimer.IsEnabled = true; }

        if (coins != null)
        {
            Coins.Clear();
            coins.ToList().ForEach(c => Coins.Add(new CoinViewModel(c)));
        }

        if (referenceCurrencies != null)
        {
            ReferenceCurrencies.Clear();
            referenceCurrencies.ToList().ForEach(rc => ReferenceCurrencies.Add(rc));
        }

        CurrentDataState = DataState.Available;
    }

    private void UpdateTimer_Tick(object? sender, EventArgs e) => BeginDataLoading(false);

    private void BeginDataLoading(bool setLoadingState = true)
    {
        if (setLoadingState)
            CurrentDataState = DataState.Loading;
        UpdateTimer.IsEnabled = false;
        _ = LoadData();
    }

    partial void OnSearchQueryChanged(string? oldValue, string newValue)
    {
        if (oldValue == newValue)
            return;

        if (string.IsNullOrEmpty(newValue))
        {
            SearchResults = [];
            return;
        }

        _coinService.SearchCoin(newValue).ContinueWith(async (res) =>
        {
            IEnumerable<Coin> coins = await res;
            SearchResults = coins.Select(c => c.Id);
            
            BeginDataLoading();
        });
    }

    [RelayCommand]
    private void RowSelected(int index)
    {
        var selectedCoin = Coins[index];
        _navigationService.NavigateTo<CoinDetailsViewModel>(selectedCoin.Id);
    }

    protected override void OnActivated()
    {
        UpdateTimer.Start();
        base.OnActivated();
    }

    protected override void OnDeactivated()
    {
        UpdateTimer.Stop();
        base.OnDeactivated();
    }
}
