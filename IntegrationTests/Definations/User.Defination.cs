using System.Net;
using Microsoft.AspNetCore.Identity.Data;
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

public record LoginUserDef
{
    public required LoginUserRequestDto LoginPayload { get; init; }
    public required HttpStatusCode HttpStatusCode { get; init; }
    public required HttpStatusCode ResponseStatusCode { get; init; }
    public required LoginUserResponseDto? LoginResponsePayload { get; init; }
    public string? ResponseMessage { get; init; }
    public bool IsValid { get; init; }
};
