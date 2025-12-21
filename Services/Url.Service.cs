using Microsoft.EntityFrameworkCore;
using UrlShortner.Helper;
using UrlShortner.Models;
using UrlShortner.Entities;
using UrlShortner.Interfaces;

namespace UrlShortner.Services;

public class UrlService
{
    private readonly IUrlRepository _urlRepository;

    public UrlService(IUrlRepository repository)
    {
        _urlRepository = repository;
    }

    public async Task CreateShortUrlAsync(AddUrlDto body)
    {
        string shortCode = Hash.Encode(body.Url);
        DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow);

        Url url = new Url
        {
            CreatedAt = date,
            UpdatedAt = date,
            IsActive = true,
            OriginalUrl = body.Url,
            UserId = null,
            ShortCode = shortCode,
        };

        await _urlRepository.CreateShortUrlAsync(url);
    }

    public async Task<ShortCodeDetails> GetShortCodeDetailsAsync(string shortCode)
    {
        return await _urlRepository.GeturlShortCodeDetailsAsync(shortCode);
    }

    // public async Task<Url> GetShortUrlByCodeAsync(string shortCode)
    // {
    //     Url url = await _urlDbSet.Where(u => u.ShortCode == shortCode).FirstOrDefaultAsync() ?? new Url();

    //     return url;
    // }

    // public async Task<List<Url>> GetAllUrlsAsync()
    // {
    //     return await _urlDbSet.ToListAsync();
    // }
}