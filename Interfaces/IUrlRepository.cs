using UrlShortner.Entities;
using UrlShortner.Models;

namespace UrlShortner.Interfaces;

public interface IUrlRepository
{
    Task CreateShortUrlAsync(Url url);
    Task<ShortCodeDetails> GeturlShortCodeDetailsAsync(string urlShortCode);
}