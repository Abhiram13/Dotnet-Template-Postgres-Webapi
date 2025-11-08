using UrlShortner;
using Abhiram.Abstractions.Logging;
using Abhiram.Extensions.DotEnv;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UrlShortner.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
DotEnvironmentVariables.Load();

builder.AddConsoleGoogleSeriLog(template: "[{Level:u3}] [TraceId: {trace_id}] [Source: {SourceContext}] {Message:lj}{NewLine}{Exception}");
builder.Services.AddRouting();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UrlService>();
builder.Services.AddScoped<MetaDataService>();
builder.Services.AddDbContext<UrlDbContext>(op => op.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")!));
builder.WebHost.ConfigureKestrel((_, server) =>
{
    string portNumber = Environment.GetEnvironmentVariable("PORT") ?? "3000";
    int port = int.Parse(portNumber);
    server.Listen(IPAddress.Any, port);
});

WebApplication app = builder.Build();

using (IServiceScope? scope = app.Services.CreateScope())
{
    try
    {
        UrlDbContext context = scope.ServiceProvider.GetRequiredService<UrlDbContext>();
        context.Database.Migrate();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
