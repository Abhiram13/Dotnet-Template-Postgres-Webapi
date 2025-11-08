using System.Data.Entity.Core;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Models;

namespace UrlShortner.Services;

public class MetaDataService
{
    private readonly DbSet<UrlMetaData> _metaDataDbSet;
    private readonly UrlDbContext _context;

    public MetaDataService(UrlDbContext context)
    {
        _context = context;
        _metaDataDbSet = context.UrlMetaData;
    }

    public async Task<UrlMetaData?> GetMetaDataByUrlIdAsync(int urlId)
    {
        Debug.Assert(urlId != 0, $"Give URL ID {urlId} is either invalid");
        UrlMetaData? urlMetaData = await _metaDataDbSet.Where(m => m.UrlId == urlId).FirstOrDefaultAsync();

        return urlMetaData;
    }

    public async Task<UrlMetaData> CreateMetaData(int urlId)
    {
        DateTime date = DateTime.UtcNow;
        UrlMetaData data = new UrlMetaData
        {
            CreatedAt = date,
            UrlId = urlId,
            Visits = 1,
            UpdatedAt = date,
        };

        await _metaDataDbSet.AddAsync(data);
        await _context.SaveChangesAsync();

        return data;
    }

    public async Task<UrlMetaData> UpdateMetaData(int urlId)
    {
        UrlMetaData? existing = await _metaDataDbSet.Where(m => m.UrlId == urlId).FirstOrDefaultAsync();

        if (existing is null) throw new ObjectNotFoundException($"Url MetaData with given id {urlId} is not found");

        existing.UpdatedAt = DateTime.UtcNow;
        existing.Visits += 1;

        await _context.SaveChangesAsync();

        return existing;
    }
}