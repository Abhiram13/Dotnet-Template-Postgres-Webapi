using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UrlShortner.Entities;

[Table("url_metadata")]
public class UrlMetaData : BaseEntity
{
    [Column("visits")]
    [JsonPropertyName("visits")]
    public int Visits { get; set; } = 0;

    [Column("url_id")]
    [JsonPropertyName("url_id")]
    [ForeignKey(nameof(Url))]
    public int UrlId { get; set; }

    public Url Url { get; set; } = default!;
}