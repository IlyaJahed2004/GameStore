using System.Reflection;
using GameStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.data;



// -------------------- Notes on GameStoreContext --------------------
// - Inherits from DbContext, which is EF Core’s main class for working with a database.
// - Represents the database used by the app.
// - DbSet<Game> games => Set<Game>();
//     • Defines a "Games" table in the database.
//     • Each row in the table is represented as a Game object.
// -------------------------------------------------------------------


public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options)
        : base(options) { }

    public DbSet<Game> games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
