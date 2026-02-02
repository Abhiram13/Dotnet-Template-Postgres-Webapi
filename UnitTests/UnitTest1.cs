using Moq;
using UrlShortner.Entities;
using UrlShortner.Helper;
using UrlShortner.Interfaces;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UnitTests;

public class UnitTest1
{
    [Theory]
    [InlineData("https://www.google.com")]
    public void TestShortCodeGenerator(string url)
    {
        string helper = Hash.Encode(url);
        string encode1 = Hash.Encode("http://www.google.com");
        string encode2 = Hash.Encode("http://www.google.com");
        
        Assert.NotEqual(encode2, encode1);
        Assert.Equal(6, helper.Length);
        Assert.Equal(6, encode1.Length);
        Assert.Equal(6, encode2.Length);
    }

    [Fact]
    public async Task TestUrlRepoAsync()
    {
        Mock<IUrlRepository> repo = new Mock<IUrlRepository>();
        Url url = new Url
        {
            CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
            UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
            IsActive = false,
            OriginalUrl = "http://www.google.com",
            ShortCode = "abcdef",
            UserId = null,
        };
        repo.Setup(r => r.CreateShortUrlAsync(url)).Returns(Task.CompletedTask);
        
        // UrlService service = new UrlService(repo.Object);
        // await service.CreateShortUrlAsync(new AddUrlDto { Url = "abcdef" });
    }
}