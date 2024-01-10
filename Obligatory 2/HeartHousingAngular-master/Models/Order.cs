using System.Text.Json.Serialization;

namespace HeartHousingAngular.Models;

public class Order
{
    [JsonPropertyName("OrderId")]
    public int OrderId { get; set; }

    [JsonPropertyName("NightsNr")]
    public int NightsNr { get; set; }

    [JsonPropertyName("TotalPrice")]
    public int TotalPrice { get; set; }

    [JsonPropertyName("RentalId")]
    //RentalID ForeignKey
    public int RentalId { get; set; }
}