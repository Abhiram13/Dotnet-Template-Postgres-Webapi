using UrlShortner.Entities;

namespace UrlShortner.Interfaces;

public interface IUrlMetaDataRepository
{
    Task CreateMetadataAsync(UrlMetaData payload);
    Task<UrlMetaData> UpdateVisitsAsync(int urlId);
}