using System.Net;

namespace IntegrationTests.Definations;

public record CreateUrlDef
{
    public required string Url { get; init; }
    public required HttpStatusCode HttpStatusCode { get; init; }
    public required HttpStatusCode ResponseStatusCode { get; init; }
    public required string ResponseMessage { get; init; }
    public bool IsAuth { get; init; }
};

public record ShortCodeDetailsDef
{
    public HttpStatusCode HttpStatusCode { get; init; }
    public HttpStatusCode ResponseStatusCode { get; init; }
    public int TotalVisits  { get; init; }
    public bool IsAuth  { get; init; }
}