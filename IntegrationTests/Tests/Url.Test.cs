using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Data;
using IntegrationTests.Definations;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Models;

namespace IntegrationTests.Tests;

public class UrlApiTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public UrlApiTests(TestWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Theory]
    [ClassData(typeof(CreateUrlData))]
    public async Task CreateUrl_UnAuth_Async(CreateUrlUnAuthDef payload)
    {
        AddUrlDto request = new AddUrlDto { Url = payload.url };
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/urls", request);

        Assert.Equal(payload.httpStatusCode, response.StatusCode);

        ApiResponse? result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        
        Assert.NotNull(result);
        Assert.Equal(payload.responseStatusCode, result!.StatusCode);        
    }
}