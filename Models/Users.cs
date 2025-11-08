using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UrlShortner.Models;

public abstract class DBTable
{
    [Key]
    [Column("id")]
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [Column("created_at")]
    [JsonPropertyName("created_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    [JsonPropertyName("updated_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}

public enum Roles
{
    Admin = 1,
    Users = 2
}

[Table("users")]
public class Users : DBTable
{
    public string Name { get; set; } = string.Empty;
    public Roles Role { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;    
}