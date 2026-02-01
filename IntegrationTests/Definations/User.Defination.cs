using System.Net;
using UrlShortner.Models;

namespace IntegrationTests.Definations;

public record class CreateUserDef
(
    CreateUserDto  createUserPayload,
    HttpStatusCode httpStatusCode,
    HttpStatusCode responseStatusCode
);

