using Microsoft.EntityFrameworkCore;
using UrlShortner.Models;

namespace UrlShortner;

public class UrlDbContext : DbContext
{
    public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }
    
    public DbSet<Url> UrlDbSet { get; set; }
    public DbSet<UrlMetaData> UrlMetaData { get; set; }
}