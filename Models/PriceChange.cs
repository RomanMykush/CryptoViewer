using Newtonsoft.Json;

namespace CryptoViewer.Models;

public class PriceChange
{
    [JsonProperty("price_change_percentage_24h_in_currency")]
    public float? Today { get; set; }
    [JsonProperty("price_change_percentage_7d_in_currency")]
    public float? InWeek { get; set; }
    [JsonProperty("price_change_percentage_30d_in_currency")]
    public float? InMonth { get; set; }
    [JsonProperty("price_change_percentage_1y_in_currency")]
    public float? InYear { get; set; }
}
