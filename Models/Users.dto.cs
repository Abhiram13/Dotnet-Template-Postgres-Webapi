using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UrlShortner.Models;

public enum Roles
{
    Admin = 1,
    Users = 2
}

[Table("users")]
public class Users : DBTable
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

public class LoginDto
{
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}