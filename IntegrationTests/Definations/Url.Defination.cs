using System.Net;

namespace IntegrationTests.Definations;

public record class CreateUrlUnAuthDef
(
    string url,
    HttpStatusCode httpStatusCode,
    HttpStatusCode responseStatusCode
);