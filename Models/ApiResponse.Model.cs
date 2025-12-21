using System.Net;
using System.Text.Json.Serialization;

namespace UrlShortner.Models;

public record class ApiResponse<T> where T : class
{
    public HttpStatusCode StatusCode { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; init; } = null;

    public required T Result { get; init; }
}

public record class ApiResponse
{
    public HttpStatusCode StatusCode { get; init; }    
    public string? Message { get; init; } = null;
}