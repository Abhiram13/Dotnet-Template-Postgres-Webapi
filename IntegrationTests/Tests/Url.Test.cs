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
    [ClassData(typeof(CreateUrlUnAuthData))]
    public async Task CreateUrl_UnAuth_Async(CreateUrlUnAuthDef payload)
    {
        AddUrlDto request = new AddUrlDto { Url = payload.Url };
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/urls", request);
        ApiResponse? result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        
        Assert.NotNull(result);
        Assert.Equal(payload.ResponseStatusCode, result!.StatusCode);    
        Assert.Equal(payload.ResponseMessage, result.Message);
        Assert.Equal(payload.HttpStatusCode, response.StatusCode);
    }
}