using Newtonsoft.Json;

namespace CryptoViewer.Dtos;

public class SimpleCoinDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Symbol { get; set; }
    [JsonProperty("large")]
    public required string ImageUri { get; set; }
}
