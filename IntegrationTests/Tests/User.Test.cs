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

    [Theory]
    [ClassData(typeof(LoginUserData))]
    public async Task LoginUserAsync(LoginUserDef payload)
    {
        await using (new UserTestsDisposal(_factory))
        {
            LoginUserRequestDto loginRequestPayload = payload.LoginPayload;
            CreateUserDto createUserPayload = new  CreateUserDto { Name = "John", Password = "123", Role = Roles.Admin,  Username = "john" };
            HttpResponseMessage createUserHttpResponse = await _httpClient.PostAsJsonAsync("/api/users/", createUserPayload);
            ApiResponse? createUserApiResponse = await createUserHttpResponse.Content.ReadFromJsonAsync<ApiResponse>();
            HttpResponseMessage loginUserHttpResponse = await _httpClient.PostAsJsonAsync("/api/users/login", loginRequestPayload);
            
            Assert.NotNull(createUserApiResponse);
            Assert.NotNull(createUserApiResponse.Message);

            if (payload.IsValid)
            {
                ApiResponse<LoginUserResponseDto>? loginUserApiResponse = await loginUserHttpResponse.Content.ReadFromJsonAsync<ApiResponse<LoginUserResponseDto>>();
                Assert.NotNull(loginUserApiResponse);
                Assert.NotNull(loginUserApiResponse?.Result);
                Assert.NotNull(loginUserApiResponse.Result.Token);
                Assert.NotEmpty(loginUserApiResponse.Result.Token);
                Assert.Equal(payload.LoginPayload.Username, loginUserApiResponse.Result.Username);
                Assert.Equal(payload.ResponseStatusCode, loginUserApiResponse.StatusCode);
            }
            else
            {
                ApiResponse? loginUserApiResponse = await loginUserHttpResponse.Content.ReadFromJsonAsync<ApiResponse>();
                Assert.NotNull(loginUserApiResponse);
                Assert.NotNull(loginUserApiResponse.Message);
                Assert.Equal(payload.ResponseMessage, loginUserApiResponse.Message);
                Assert.Equal(payload.ResponseStatusCode, loginUserApiResponse.StatusCode);
            }
            
            Assert.Equal(HttpStatusCode.Created, createUserApiResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Created, createUserHttpResponse.StatusCode);
            Assert.Equal(payload.HttpStatusCode, loginUserHttpResponse.StatusCode);
        }
    }
}

