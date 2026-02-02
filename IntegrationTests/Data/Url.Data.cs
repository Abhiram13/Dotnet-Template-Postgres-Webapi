using System.Net;
using IntegrationTests.Definations;
using UrlShortner.Entities;

namespace IntegrationTests.Data;

public abstract class TheoryTestData<T> : TheoryData<T> where T : class 
{
    protected readonly DateOnly _currenDate = DateOnly.FromDateTime(DateTime.UtcNow); 
}

public class CreateUrlData : TheoryTestData<CreateUrlDef>
{
    public CreateUrlData()
    {
        SetData();
    }

    private void SetData()
    {
        CreateUrlDef payload = new CreateUrlDef
        {
            Url = "http://www.google.com",
            HttpStatusCode = HttpStatusCode.Unauthorized,
            ResponseStatusCode = HttpStatusCode.Unauthorized,
            ResponseMessage = "Invalid credentials provided",
            IsAuth = false
        };
        Add(payload);

        CreateUrlDef authPayload = new CreateUrlDef
        {
            Url = "http://www.google.com",
            HttpStatusCode = HttpStatusCode.Created,
            ResponseStatusCode = HttpStatusCode.Created,
            ResponseMessage = "Short Url is successfully created",
            IsAuth = true
        };
        Add(authPayload);
    }
}

public class ShortCodeDetailsData : TheoryTestData<ShortCodeDetailsDef>
{
    public ShortCodeDetailsData()
    {
        SetData();
    }

    private void SetData()
    {
        ShortCodeDetailsDef detail1 = new ShortCodeDetailsDef
        {
            HttpStatusCode = HttpStatusCode.OK,
            ResponseStatusCode = HttpStatusCode.OK,
            TotalVisits = 5,
            IsAuth = true
        };
        Add(detail1);
    }
}