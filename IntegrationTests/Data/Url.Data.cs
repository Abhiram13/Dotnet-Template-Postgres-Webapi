using System.Net;
using IntegrationTests.Definations;
using UrlShortner.Entities;

namespace IntegrationTests.Data;

public abstract class TheoryTestData<T> : TheoryData<T> where T : class 
{
    protected readonly DateOnly _currenDate = DateOnly.FromDateTime(DateTime.UtcNow); 
}

public class CreateUrlData : TheoryTestData<CreateUrlUnAuthDef>
{
    public CreateUrlData()
    {
        Add(new CreateUrlUnAuthDef(url: "http://www.google.com", httpStatusCode: HttpStatusCode.Unauthorized, responseStatusCode: HttpStatusCode.Unauthorized));
    }
}