using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UrlShortner.Models;

[Table("urls")]
public class Url : DBTable
{
    [Column("original_url")]
    [JsonPropertyName("original_url")]
    public string OriginalUrl { get; set; } = string.Empty;

    [Column("short_code")]
    [JsonPropertyName("short_code")]
    public string ShortCode { get; set; } = string.Empty;

    [Column("user_id")]
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    [Column("is_active")]
    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }
}

[Table("url_metadata")]
public class UrlMetaData : DBTable
{
    [Column("visits")]
    [JsonPropertyName("visits")]
    public int Visits { get; set; } = 0;

    [Column("url_id")]
    [JsonPropertyName("url_id")]
    public int UrlId { get; set; }
}

public class AddUrlDto
{
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}