using CryptoViewer.Models;

namespace CryptoViewer.Services.CoinService;

public interface ICoinService
{
    Task<IEnumerable<Coin>> GetTopNCoins(int num);
    Task<IEnumerable<Coin>> GetCoins(IEnumerable<string> ids);
    Task<IEnumerable<Coin>> SearchCoin(string query);
}
