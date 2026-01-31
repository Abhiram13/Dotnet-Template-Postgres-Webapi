using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UrlShortner;

namespace IntegrationTests;

public static class TestDbContextFactory
{
    public static UrlDbContext Create()
    {
        IConfigurationRoot? configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(path: "./appsettings.Test.json", optional: false)
            .Build();

        string? connectionString = configuration.GetConnectionString("TestDb");

        DbContextOptions<UrlDbContext>? options = new DbContextOptionsBuilder<UrlDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        UrlDbContext context = new UrlDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }
}

public abstract class TestBase : IDisposable
{
    protected readonly UrlDbContext _dbContext;
    
    protected TestBase()
    {
        _dbContext = TestDbContextFactory.Create();
        _dbContext.Database.BeginTransaction();
    }

    public void Dispose()
    {
        _dbContext.Database.RollbackTransaction();
        _dbContext.Dispose();
    }
}
