using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Models;

namespace UrlShortner;

public class UrlDbContext : DbContext
{
    public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }
    
    public DbSet<Url> UrlDbSet { get; init; }
    public DbSet<UrlMetaData> UrlMetaData { get; init; }
    public DbSet<Users> UsersDbSet { get; init; }
}