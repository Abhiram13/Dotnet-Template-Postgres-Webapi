using Microsoft.EntityFrameworkCore;
using UrlShortner.Helper;
using UrlShortner.Models;

namespace UrlShortner.Services;

public class UrlService
{
    private readonly DbSet<Url> _urlDbSet;
    private readonly UrlDbContext _context;

    public UrlService(UrlDbContext context)
    {
        _context = context;
        _urlDbSet = context.UrlDbSet;
    }

    public async Task<Url> CreateShortUrlAsync(AddUrlDto body)
    {
        string shortCode = Hash.Encode(body.Url);

        Url url = new Url();
        url.CreatedAt = DateTime.UtcNow;
        url.UpdatedAt = DateTime.UtcNow;
        url.IsActive = true;
        url.OriginalUrl = body.Url;
        url.UserId = null;
        url.ShortCode = shortCode;

        await _urlDbSet.AddAsync(url);
        await _context.SaveChangesAsync();

        return url;
    }

    public async Task<Url> GetShortUrlByCodeAsync(string shortCode)
    {
        Url url = await _urlDbSet.Where(u => u.ShortCode == shortCode).FirstOrDefaultAsync() ?? new Url();

        return url;
    }

    public async Task<List<Url>> GetAllUrlsAsync()
    {
        return await _urlDbSet.ToListAsync();
    }
}