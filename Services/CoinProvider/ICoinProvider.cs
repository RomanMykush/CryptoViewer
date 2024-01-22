using CryptoViewer.Dtos;

namespace CryptoViewer.Services.CoinProvider;

public interface ICoinProvider
{
    Task<IEnumerable<SimpleCoinDto>> SearchCoin(string query);
    Task<IEnumerable<CoinDto>> GetCoins(string referenceCurrency, ICollection<string>? ids = null);
}
