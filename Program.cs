using UrlShortner;
using Abhiram.Abstractions.Logging;
using Abhiram.Extensions.DotEnv;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UrlShortner.Services;
using Microsoft.AspNetCore.Identity;
using UrlShortner.Models;
using UrlShortner.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string jwtKey = builder.Configuration["Jwt:Key"]!;
string jwtIssuer = builder.Configuration["Jwt:Issuer"]!;

DotEnvironmentVariables.Load();

builder.AddConsoleGoogleSeriLog(template: "[{Level:u3}] [TraceId: {trace_id}] [Source: {SourceContext}] {Message:lj}{NewLine}{Exception}");
builder.Services.AddRouting();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UrlService>();
builder.Services.AddScoped<MetaDataService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<Jwt>();
builder.Services.AddSingleton<PasswordHash>();
builder.Services.AddDbContext<UrlDbContext>(op => op.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")!));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
    };
});
builder.Services.AddAuthorization();
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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
