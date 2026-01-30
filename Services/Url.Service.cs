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

    public async Task<string?> GetLongUrlAsync(string shortCode)
    {
        LongUrlDetails? result = await _urlRepository.GetLongUrlAsync(shortCode);
        return result?.LongUrl;
    }
}