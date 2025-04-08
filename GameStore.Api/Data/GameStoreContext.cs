using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// A DbContext instance represents a session with the database and can be used to query and save instances of your entities. 
// DbContext is a combination of the Unit Of Work and Repository patterns.
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    // DbSet properties represent collections of entities in the database. 
    // Each DbSet corresponds to a table in the database.
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Fluent API configuration for the Game entity
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Action" },
            new { Id = 2, Name = "Adventure" },
            new { Id = 3, Name = "RPG" },
            new { Id = 4, Name = "Strategy" },
            new { Id = 5, Name = "Simulation" }
        );

    }

}
