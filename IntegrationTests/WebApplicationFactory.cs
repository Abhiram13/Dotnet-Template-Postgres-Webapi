using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using UrlShortner;
using Abhiram.Extensions.DotEnv;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    public UrlDbContext GetDbContext()
    {
        return Services.CreateScope().ServiceProvider.GetRequiredService<UrlDbContext>();
    }

    public IServiceScope CreateScope()
    {
        return Services.CreateScope();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        DotEnvironmentVariables.Load();

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddEnvironmentVariables();
        });

        builder.ConfigureServices(services =>
        {
            ServiceDescriptor descriptor = services.Single(s => s.ServiceType == typeof(DbContextOptions<UrlDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<UrlDbContext>(option =>
            {
                option.UseNpgsql(TestDbContextFactory.GetConnectionString());
            });
        });
    }
}