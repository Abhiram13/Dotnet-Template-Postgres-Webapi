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
        _urlDbSet.Add(url);
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

    public async Task<Url?> GetLongUrlAsync(string shortCode)
    {
        Url? longUrl = await _urlDbSet.Where(u => u.ShortCode == shortCode && u.IsActive == true).FirstOrDefaultAsync();
        
        if (longUrl == null) return null;

        // UrlMetaData? metaData = await _urlMetaData.Where(m => m.UrlId == longUrl.Id).FirstOrDefaultAsync();
        // DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow);
        // metaData.UpdatedAt = date;
        // metaData.Visits = ++metaData.Visits;
        //
        // await _dbContext.SaveChangesAsync();

        return longUrl;
    }
}