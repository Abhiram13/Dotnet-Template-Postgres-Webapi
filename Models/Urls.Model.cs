using System.Text.Json.Serialization;

namespace UrlShortner.Models;

public record class AddUrlDto
{
    public string Url { get; set; } = string.Empty;
}

public record class ShortCodeDetails
{
    public required string ShortCode { get; init; }
    public required string OriginalUrl { get; init; }
    public int TotalVisits { get; init; }
    public bool IsActive { get; init; }
}