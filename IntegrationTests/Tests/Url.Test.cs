using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using IntegrationTests.Data;
using IntegrationTests.Definations;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Enums;
using UrlShortner.Models;

namespace IntegrationTests.Tests;

public class UrlApiTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public UrlApiTests(TestWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
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
}