using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortner;
using UrlShortner.Entities;

namespace IntegrationTests;

public sealed class UserTestsDisposal : IAsyncDisposable
{
    private readonly TestWebApplicationFactory _factory;

    public UserTestsDisposal(TestWebApplicationFactory factory)
    {
        _factory = factory;
    }
    
    public async ValueTask DisposeAsync()
    {
        using (IServiceScope scope = _factory.Services.CreateScope())
        {
            UrlDbContext db = scope.ServiceProvider.GetRequiredService<UrlDbContext>();
            await db.UsersDbSet.ExecuteDeleteAsync();
        }
    }
}

public sealed class UrlTestsDisposal : IAsyncDisposable
{
    private readonly TestWebApplicationFactory _factory;

    public UrlTestsDisposal(TestWebApplicationFactory factory)
    {
        _factory = factory;
    }
    
    public async ValueTask DisposeAsync()
    {
        using (IServiceScope scope = _factory.Services.CreateScope())
        {
            UrlDbContext db = scope.ServiceProvider.GetRequiredService<UrlDbContext>();
            await db.UrlDbSet.ExecuteDeleteAsync();
            await db.UrlMetaData.ExecuteDeleteAsync();
        }
    }
}