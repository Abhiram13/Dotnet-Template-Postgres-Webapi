using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Models;

namespace IntegrationTests;

public class UrlApiTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public UrlApiTests(TestWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task CreateUrlAsync()
    {
        AddUrlDto request = new AddUrlDto { Url = "https://google.com" };
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/urls", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        ApiResponse? result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Created, result!.StatusCode);        
    }
}