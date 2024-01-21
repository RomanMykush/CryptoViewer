using Newtonsoft.Json;

namespace CryptoViewer.Models;

public class Coin : IExpirable
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }
    public required string ImageUri { get; set; }

    public float Price { get; set; }
    public float Volume { get; set; }
    public float CirculatingSupply { get; set; }
    public PriceChange PriceChange { get; set; } = null!;
    public float MarketCap => Price * CirculatingSupply;

    public DateTime ExpirationDate { get; }
    public Coin(TimeSpan lifeSpan)
    {
        ExpirationDate = DateTime.UtcNow + lifeSpan;
    }
}
