using System.Net;
using IntegrationTests.Definations;
using UrlShortner.Entities;

namespace IntegrationTests.Data;

public abstract class TheoryTestData<T> : TheoryData<T> where T : class 
{
    protected readonly DateOnly _currenDate = DateOnly.FromDateTime(DateTime.UtcNow); 
}

public class CreateUrlUnAuthData : TheoryTestData<CreateUrlUnAuthDef>
{
    public CreateUrlUnAuthData()
    {
        SetData();
    }

    private void SetData()
    {
        CreateUrlUnAuthDef payload = new CreateUrlUnAuthDef
        {
            Url = "http://www.google.com",
            HttpStatusCode = HttpStatusCode.Unauthorized,
            ResponseStatusCode = HttpStatusCode.Unauthorized,
            ResponseMessage = "Invalid credentials provided"
        };
        Add(payload);
    }
}