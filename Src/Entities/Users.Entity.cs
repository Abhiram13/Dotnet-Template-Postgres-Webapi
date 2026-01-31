using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UrlShortner.Enums;

namespace UrlShortner.Entities;

[Table("users")]
public class Users : BaseEntity
{
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Column("role")]
    [JsonPropertyName("role")]
    public Roles Role { get; set; }

    [Column("username")]
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    [Column("password")]
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}