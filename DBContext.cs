using Microsoft.EntityFrameworkCore;
using UrlShortner.Models;

namespace UrlShortner;

public class UrlDbContext : DbContext
{
    public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }
    
    public required DbSet<Url> UrlDbSet { get; init; }
    public required DbSet<UrlMetaData> UrlMetaData { get; init; }
    public required DbSet<Users> UsersDbSet { get; init; }
}