using Microsoft.EntityFrameworkCore;
public class SqliteDbContext : DbContext
{
    public DbSet<Contact> contacts { get; set; }

    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }
}
