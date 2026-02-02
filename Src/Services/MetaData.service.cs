using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Interfaces;
using UrlShortner.Models;

namespace UrlShortner.Services;

public class MetaDataService
{
    private readonly DbSet<UrlMetaData> _metaDataDbSet;
    private readonly UrlDbContext _context;
    private readonly IUrlMetaDataRepository  _urlMetaDataRepository;

    public MetaDataService(UrlDbContext context, IUrlMetaDataRepository  urlMetaDataRepository)
    {
        _context = context;
        _metaDataDbSet = context.UrlMetaData;
        _urlMetaDataRepository = urlMetaDataRepository;
    }

    public async Task<UrlMetaData?> GetMetaDataByUrlIdAsync(int urlId)
    {
        Debug.Assert(urlId != 0, $"Give URL ID {urlId} is either invalid");
        UrlMetaData? urlMetaData = await _metaDataDbSet.Where(m => m.UrlId == urlId).FirstOrDefaultAsync();

        return urlMetaData;
    }

    public async Task<UrlMetaData> CreateMetaData(int urlId)
    {
        DateOnly date = DateOnly.FromDateTime(DateTime.UtcNow);
        UrlMetaData data = new UrlMetaData
        {
            CreatedAt = date,
            UrlId = urlId,
            Visits = 1,
            UpdatedAt = date,
        };

        await _urlMetaDataRepository.CreateMetadataAsync(data);
        return data;
    }

    public async Task<UrlMetaData> UpdateMetaData(int urlId)
    {
        UrlMetaData? existing = await _urlMetaDataRepository.UpdateVisitsAsync(urlId);
        return existing;
    }

    public async Task<UrlMetaData> SaveAsync(int urlId)
    {
        UrlMetaData? existing = await GetMetaDataByUrlIdAsync(urlId);
        UrlMetaData result = existing is null ? await CreateMetaData(urlId) : await UpdateMetaData(urlId);

        return result;
    }
}