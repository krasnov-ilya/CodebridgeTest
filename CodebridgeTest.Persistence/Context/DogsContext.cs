using CodebridgeTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodebridgeTest.Persistence.Context;

public class DogsContext : DbContext
{
    private const string ConnectionString = "Data Source=dogs.db";
    
    public DbSet<Dog> Dogs { get; set; } = null!;

    public DogsContext() { }
    
    public DogsContext(DbContextOptions options) 
        : base(options)
    {
        base.Database.Migrate();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);
    }
}