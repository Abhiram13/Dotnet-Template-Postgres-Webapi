using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;

namespace IntegrationTests;

public class UrlTests : TestBase
{
    public async Task AddUrl()
    {
        Url url = new Url
        {
           CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
           UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
           IsActive = true,
           OriginalUrl = "http://google.com",
           ShortCode = "abcd",
           UserId = 1,                        
        };

        await _dbContext.AddAsync(url);
        await _dbContext.SaveChangesAsync();

        Url? savedUrl = await _dbContext.UrlDbSet.FirstAsync();

        Assert.Equal("abcd", savedUrl.ShortCode);
    }
}