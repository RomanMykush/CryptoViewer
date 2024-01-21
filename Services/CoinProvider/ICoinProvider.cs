using CryptoViewer.Dtos;

namespace CryptoViewer.Services.CoinProvider;

public interface ICoinProvider
{
    Task<ICollection<SimpleCoinDto>> SearchCoin(string query);
    Task<ICollection<CoinDto>> GetCoins(string referenceCurrency, ICollection<string>? ids = null);
    Task<ICollection<string>> GetReferenceCurrencies();
}
