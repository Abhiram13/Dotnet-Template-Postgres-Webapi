using Microsoft.EntityFrameworkCore;

// TODO: RENAME THIS NAMESPACE TO MATCH PROJECT STRUCTURE
namespace PostgresWebApiTemplate;

// TODO: RENAME THIS CLASS TO MATCH PROJECT STRUCTURE
public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    // TODO: ADD DB SET PROPERTIES
    // public DbSet<T> Property { get; set; }
}