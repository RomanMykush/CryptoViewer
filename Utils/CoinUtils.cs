using CryptoViewer.Models;

namespace CryptoViewer.Utils;

public static class CoinUtils
{
    public static bool AreExpired(this IEnumerable<Coin> coins) => coins.Any(c => c.IsExpired());
}
