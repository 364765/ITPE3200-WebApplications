using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace HeartHousingAngular.Models;

public class Rental
{
    [JsonPropertyName("RentalId")]
    public int RentalId { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Address")]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("PricePrNight")]
    public int PricePrNight { get; set; }

    [JsonPropertyName("RentalType")]
    public string RentalType { get; set; } = string.Empty;

    [JsonPropertyName("BedNr")]
    public int BedNr { get; set; }

    [JsonPropertyName("BathNr")]
    public int BathNr { get; set; }

    [JsonPropertyName("Area")]
    public int Area { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("ImageUrl")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("ImageUrl2")]
    public string? ImageUrl2 { get; set; }

    [JsonPropertyName("ImageUrl3")]
    public string? ImageUrl3 { get; set; }

}
