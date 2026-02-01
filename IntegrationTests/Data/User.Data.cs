using System.Net;
using IntegrationTests.Definations;
using UrlShortner.Enums;
using UrlShortner.Models;

namespace IntegrationTests.Data;

public class CreateUserData : TheoryTestData<CreateUserDef>
{
    public CreateUserData()
    {
        SetData();
    }

    private void SetData()
    {
        CreateUserDef validPayload = new CreateUserDef
        {
            CreateUserPayload = new CreateUserDto { Name = "John", Username = "john", Password = "123", Role = Roles.Admin},
            HttpStatusCode = HttpStatusCode.Created,
            ResponseStatusCode = HttpStatusCode.Created,
            ResponseMessage = "New user created successfully"
        };
        Add(validPayload);

        CreateUserDef noNameBadRequestPayload = new CreateUserDef
        {
            CreateUserPayload = new CreateUserDto { Name = "", Password = "123", Role = Roles.Admin, Username = "john"},
            HttpStatusCode = HttpStatusCode.BadRequest,
            ResponseStatusCode = HttpStatusCode.BadRequest,
            ResponseMessage = "Invalid request provided"
        };
        Add(noNameBadRequestPayload);
        
        CreateUserDef noUserNameBadRequestPayload = new CreateUserDef
        {
            CreateUserPayload = new CreateUserDto { Name = "John", Password = "123", Role = Roles.Admin, Username = ""},
            HttpStatusCode = HttpStatusCode.BadRequest,
            ResponseStatusCode = HttpStatusCode.BadRequest,
            ResponseMessage = "Invalid request provided"
        };
        Add(noUserNameBadRequestPayload);
        
        CreateUserDef noPasswordBadRequestPayload = new CreateUserDef
        {
            CreateUserPayload = new CreateUserDto { Name = "John", Password = "", Role = Roles.Admin, Username = "john"},
            HttpStatusCode = HttpStatusCode.BadRequest,
            ResponseStatusCode = HttpStatusCode.BadRequest,
            ResponseMessage = "Invalid request provided"
        };
        Add(noPasswordBadRequestPayload);
        
        CreateUserDef duplicateUserNamePayload = new CreateUserDef
        {
            CreateUserPayload = new CreateUserDto { Name = "John", Password = "123", Role = Roles.Admin, Username = "john"},
            HttpStatusCode = HttpStatusCode.BadRequest,
            ResponseStatusCode = HttpStatusCode.BadRequest,
            ResponseMessage = "Invalid request provided",
            DuplicateRowTest = true
        };
        Add(duplicateUserNamePayload);
    }
}