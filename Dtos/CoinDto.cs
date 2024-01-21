using CryptoViewer.Models;
using Newtonsoft.Json;

namespace CryptoViewer.Dtos;

public class CoinDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }
    [JsonProperty("image")]
    public required string ImageUri { get; set; }
    [JsonProperty("current_price")]
    public required float Price { get; set; }
    [JsonProperty("total_volume")]
    public required float Volume { get; set; }
    [JsonProperty("circulating_supply")]
    public required float CirculatingSupply { get; set; }
    public PriceChange PriceChange { get; set; } = null!;
}
