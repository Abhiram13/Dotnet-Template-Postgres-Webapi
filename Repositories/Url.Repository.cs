using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Interfaces;
using UrlShortner.Models;

namespace UrlShortner.Repository;

public class UrlRepository : IUrlRepository
{
    private readonly DbSet<Url> _urlDbSet;
    private readonly DbSet<UrlMetaData> _urlMetaData;
    private readonly UrlDbContext _dbContext;

    public UrlRepository(UrlDbContext context)
    {
        _dbContext = context;
        _urlDbSet = _dbContext.UrlDbSet;
        _urlMetaData = _dbContext.UrlMetaData;
    }

    public async Task CreateShortUrlAsync(Url url)
    {
        await _urlDbSet.AddAsync(url);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ShortCodeDetails> GeturlShortCodeDetailsAsync(string urlShortCode)
    {
        ShortCodeDetails details = await _urlDbSet
            .Where(u => u.ShortCode == urlShortCode)      
            .Select(u => new ShortCodeDetails
            {
                ShortCode = u.ShortCode,
                OriginalUrl = u.OriginalUrl,
                IsActive = u.IsActive,
                TotalVisits = u.Meta.Visits
            })
            .FirstAsync();

        return details;
    }
}