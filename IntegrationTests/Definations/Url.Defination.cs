using System.Net;

namespace IntegrationTests.Definations;

public record CreateUrlUnAuthDef
{
    public required string Url { get; init; }
    public required HttpStatusCode HttpStatusCode { get; init; }
    public required HttpStatusCode ResponseStatusCode { get; init; }
    public required string ResponseMessage { get; init; }
};