using Moq;
using UrlShortner.Entities;
using UrlShortner.Helper;
using UrlShortner.Interfaces;
using UrlShortner.Models;
using UrlShortner.Services;

namespace UnitTests;

public class UnitTest1
{
    [Fact]
    public void TestShortCodeGenerator()
    {
        string helper = Hash.Encode("http://www.google.com");
        Assert.Equal(7, helper.Length);
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
        
        UrlService service = new UrlService(repo.Object);
        await service.CreateShortUrlAsync(new AddUrlDto { Url = "abcdef" });
    }
}