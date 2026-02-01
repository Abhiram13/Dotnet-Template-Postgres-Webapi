using System.Net;
using IntegrationTests.Definations;
using UrlShortner.Enums;
using UrlShortner.Models;

namespace IntegrationTests.Data;

public class CreateUserData : TheoryTestData<CreateUserDef>
{
    public CreateUserData()
    {
        Add(new CreateUserDef(
            createUserPayload: new CreateUserDto { Name = "John", Username = "john", Password = "123", Role = Roles.Admin},
            httpStatusCode: HttpStatusCode.Created,
            responseStatusCode: HttpStatusCode.Created
        ));
    }
}