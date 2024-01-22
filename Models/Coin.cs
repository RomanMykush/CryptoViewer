using Newtonsoft.Json;

namespace CryptoViewer.Models;

public class Coin : IExpirable
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }
    public required string ImageUri { get; set; }

    public float? Price { get; set; }
    public float? Volume { get; set; }
    public float? CirculatingSupply { get; set; }
    public PriceChange? PriceChange { get; set; }
    public float? MarketCap => Price * CirculatingSupply;

    public DateTime ExpirationDate { get; }
    public Coin()
    {
        ExpirationDate = DateTime.UtcNow + TimeSpan.FromMinutes(1);
    }

    public bool IsExpired()
    {
        if (ExpirationDate > DateTime.UtcNow)
            return true;
        return false;
    }

    public bool IsIncomplete() => Price == null || Volume == null ||
        CirculatingSupply == null || PriceChange == null;
}
