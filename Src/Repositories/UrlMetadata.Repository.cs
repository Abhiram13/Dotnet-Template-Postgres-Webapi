using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Interfaces;

namespace UrlShortner.Repository;

public sealed class UrlMetadataRepository : IUrlMetaDataRepository
{
    private readonly DbSet<UrlMetaData> _urlMetaData;
    private readonly UrlDbContext _dbContext;
    
    public UrlMetadataRepository(UrlDbContext context)
    {
        _dbContext = context;
        _urlMetaData = _dbContext.UrlMetaData;
    }
    
    public async Task CreateMetadataAsync(UrlMetaData payload)
    {
        await _urlMetaData.AddAsync(payload);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UrlMetaData> UpdateVisitsAsync(int urlId)
    {
        UrlMetaData? existing = await _urlMetaData.FirstOrDefaultAsync(u => u.UrlId == urlId);

        if (existing is null) throw new KeyNotFoundException($"Url MetaData with given id {urlId} is not found");

        existing.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
        existing.Visits += 1;

        await _dbContext.SaveChangesAsync();
        
        return existing;
    }
}