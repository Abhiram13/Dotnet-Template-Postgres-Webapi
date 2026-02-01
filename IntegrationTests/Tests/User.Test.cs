using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using IntegrationTests.Data;
using IntegrationTests.Definations;
using UrlShortner.Enums;
using UrlShortner.Models;

namespace IntegrationTests.Tests;

public class UserApiTest : BaseIntegrationTest, IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public UserApiTest(TestWebApplicationFactory factory) : base(factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Theory]
    [ClassData(typeof(CreateUserData))]
    public async Task CreateUserAsync(CreateUserDef payload)
    {
        await using (new UserTestsDisposal(_factory))
        {
            CreateUserDto request = payload.CreateUserPayload;

            if (payload.DuplicateRowTest)
            {
                CreateUserDto duplicateUser = new CreateUserDto { Name = "John", Password = "123", Role = Roles.Admin, Username = "john"};
                HttpResponseMessage duplicateUserHttpResponse = await _httpClient.PostAsJsonAsync("/api/users/", duplicateUser);
                ApiResponse? duplicateUserApiResponse = await duplicateUserHttpResponse.Content.ReadFromJsonAsync<ApiResponse>();
                
                Assert.NotNull(duplicateUserApiResponse);
                Assert.NotNull(duplicateUserApiResponse.Message);
                Assert.Equal("New user created successfully", duplicateUserApiResponse.Message);
                Assert.Equal(HttpStatusCode.Created, duplicateUserApiResponse.StatusCode);
                Assert.Equal(HttpStatusCode.Created, duplicateUserHttpResponse.StatusCode);
            }
            
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/users/", request);
            ApiResponse? result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            
            Assert.NotNull(result);
            Assert.NotNull(result.Message);
            Assert.Equal(payload.HttpStatusCode, response.StatusCode);
            Assert.Equal(payload.ResponseStatusCode, result.StatusCode);
            Assert.Equal(payload.ResponseMessage, result.Message);
        }
    }
}

