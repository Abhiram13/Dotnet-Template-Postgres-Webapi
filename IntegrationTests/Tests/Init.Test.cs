using System.Net;
using System.Net.Http.Json;
using UrlShortner.Models;

namespace IntegrationTests.Tests;

public sealed class InitApiTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public InitApiTests(TestWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task WelcomeApiAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/");
        ApiResponse? result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result!.StatusCode);  
        Assert.Equal("Welcome, this is URL Shortner project", result.Message);
    }
}