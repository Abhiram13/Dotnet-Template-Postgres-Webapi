using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using IntegrationTests.Data;
using IntegrationTests.Definations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Enums;
using UrlShortner.Models;

namespace IntegrationTests.Tests;

public class UrlApiTests : BaseIntegrationTest, IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public UrlApiTests(TestWebApplicationFactory factory) : base(factory)
    {
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    private async Task<string> GetToken()
    {
        CreateUserDto createUserDto = new CreateUserDto { Username = "john", Password = "123", Name = "John", Role = Roles.Admin };
        LoginUserRequestDto loginPayload = new LoginUserRequestDto { Username = "john", Password = "123" };
        await _httpClient.PostAsJsonAsync("/api/users/", createUserDto);
        HttpResponseMessage loginHttpResponse = await _httpClient.PostAsJsonAsync("/api/users/login", loginPayload);
        ApiResponse<LoginUserResponseDto>? loginApiResponse = await loginHttpResponse.Content.ReadFromJsonAsync<ApiResponse<LoginUserResponseDto>>();
        return loginApiResponse!.Result.Token;
    }

    [Theory]
    [ClassData(typeof(CreateUrlData))]
    public async Task CreateUrl_Async(CreateUrlDef payload)
    {
        AddUrlDto request = new AddUrlDto { Url = payload.Url };
        
        if (payload.IsAuth)
        {
            string token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/urls", request);
        ApiResponse? result = await response.Content.ReadFromJsonAsync<ApiResponse>();
        
        Assert.NotNull(result);
        Assert.Equal(payload.ResponseStatusCode, result!.StatusCode);    
        Assert.Equal(payload.ResponseMessage, result.Message);
        Assert.Equal(payload.HttpStatusCode, response.StatusCode);
    }

    [Fact]
    public async Task GetShortCodeUrl_Async()
    {
        const string LONG_URL = "https://www.budget-tracker.com/";
        
        // Get Token
        string token = await GetToken();
        
        // Create URL
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/urls/")
        {
            Content = JsonContent.Create(new AddUrlDto { Url = LONG_URL })
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await _httpClient.SendAsync(request);
        
        // Fetch short code from DB based on long URL
        Url url = await _dbContext.UrlDbSet.Where(u => u.OriginalUrl == LONG_URL).FirstAsync();
        HttpResponseMessage response = await _httpClient.GetAsync(url.ShortCode);
        
        // Asserts
        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.True(response.Headers.Contains("Location"));
        Assert.Equal(LONG_URL, response.Headers.GetValues("Location").First());
    }
}