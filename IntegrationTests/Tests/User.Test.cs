using System.Net.Http.Json;
using System.Text.Json;
using IntegrationTests.Data;
using IntegrationTests.Definations;
using UrlShortner.Enums;
using UrlShortner.Models;

namespace IntegrationTests.Tests;

public class UserTest : BaseIntegrationTest, IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public UserTest(TestWebApplicationFactory factory) : base(factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Theory]
    [ClassData(typeof(CreateUserData))]
    public async Task CreateUserAsync(CreateUserDef payload)
    {
        await using (new UserTestsDisposal(_factory))
        {
            CreateUserDto request = payload.createUserPayload;
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/users/", request);
            ApiResponse? result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            
            Assert.NotNull(result);
            Assert.Equal(payload.httpStatusCode, response.StatusCode);
            Assert.Equal(payload.responseStatusCode, result.StatusCode);
        }
    }
}

