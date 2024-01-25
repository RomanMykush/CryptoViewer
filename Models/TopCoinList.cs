namespace CryptoViewer.Models;

public class TopCoinList : IExpirable
{
    public required IEnumerable<string> CoinsIds { get; set; }

    public int Count { get => CoinsIds.ToList().Count; }

    public DateTime ExpirationDate { get; }

    public TopCoinList()
    {
        ExpirationDate = DateTime.UtcNow + TimeSpan.FromMinutes(5);
    }

    public bool IsExpired()
    {
        if (ExpirationDate > DateTime.UtcNow)
            return true;
        return false;
    }
}
