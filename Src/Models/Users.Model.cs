using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UrlShortner.Enums;

namespace UrlShortner.Models;

public record class CreateUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Roles Role { get; set; }
}

public record class LoginUserRequestDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public record class LoginUserResponseDto
{
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}