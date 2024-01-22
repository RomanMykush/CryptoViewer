using CommunityToolkit.Mvvm.Messaging;
using CryptoViewer.Services.ReferenceCurrencyProvider;
using Microsoft.Extensions.Options;

namespace CryptoViewer.Stores.ReferenceCurrencyStore;

public class ReferenceCurrencyStore
{
    private IEnumerable<string> ReferenceCurrencies = [];

    private string _currentRefCurrency;
    public string CurrentRefCurrency
    {
        get => _currentRefCurrency;
        set
        {
            if (_currentRefCurrency == value)
                return;

            _currentRefCurrency = value;
            WeakReferenceMessenger.Default
                .Send(new CurrentRefCurrencyChangedMessage(_currentRefCurrency));
        }
    }

    private readonly IReferenceCurrencyProvider _referenceCurrencyProvider;

    public ReferenceCurrencyStore(IReferenceCurrencyProvider referenceCurrencyProvider, IOptions<ApplicationOptions> appOptions)
    {
        _referenceCurrencyProvider = referenceCurrencyProvider;
        _currentRefCurrency = appOptions.Value.DefaultRefCurrency;
    }

    public async Task<IEnumerable<string>> GetReferenceCurrencies()
    {
        if (ReferenceCurrencies.Any())
            return ReferenceCurrencies;

        ReferenceCurrencies = await _referenceCurrencyProvider.GetReferenceCurrencies();

        return ReferenceCurrencies;
    }
}
