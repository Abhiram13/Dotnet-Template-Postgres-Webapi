using System.Net;
using UrlShortner.Models;

namespace IntegrationTests.Definations;

public record CreateUserDef
{
    public required CreateUserDto CreateUserPayload { get; init; }
    public required HttpStatusCode HttpStatusCode { get; init; }
    public required HttpStatusCode ResponseStatusCode { get; init; }
    public required string ResponseMessage { get; init; }
    public bool DuplicateRowTest { get; init; }
};

