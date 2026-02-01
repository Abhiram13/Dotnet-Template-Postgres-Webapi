using UrlShortner;

namespace IntegrationTests;

public abstract class BaseIntegrationTest
{
    protected readonly TestWebApplicationFactory _factory;
    protected readonly UrlDbContext _dbContext;

    protected BaseIntegrationTest(TestWebApplicationFactory factory)
    {
        _factory = factory;
        _dbContext = factory.GetDbContext();
    }
}