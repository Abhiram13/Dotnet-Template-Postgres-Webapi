using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UrlShortner.Entities;

[Table("urls")]
public class Url : BaseEntity
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

    public UrlMetaData Meta { get; set; } = default!;
}