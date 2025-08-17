using GameStore.data;
using GameStore.Endpoints;
using GameStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ⚠ EF Core Notes:
// 1. MaxLength / Required attributes:
//    - EF Core automatically applies [MaxLength] and [Required] in migrations,
//      so Name, Genre, imageuri columns are correctly set in the database.
// 2. Decimal Precision Warning (e.g., Price):
//    - EF Core cannot infer exact precision/scale from [Range] or decimal type.
//    - To remove the warning, explicitly configure it in the database:
//      Approach 1 (recommended): Using IEntityTypeConfiguration<T>:
//         builder.Property(g => g.Price).HasColumnType("decimal(5,2)");
//      Approach 2 (alternative): Directly in DbContext's OnModelCreating:
//         modelBuilder.Entity<Game>()
//                     .Property(g => g.Price)
//                     .HasColumnType("decimal(5,2)");
// 3. OnModelCreating:
//    - Overriding OnModelCreating does not delete EF Core's default behavior.
//    - Make sure to call ApplyConfiguration for all IEntityTypeConfiguration classes
//      you want applied; otherwise, missing configurations won't be applied.


builder.Services.AddSingleton<IGameRepository, InMemRepository>();

var connstring = builder.Configuration.GetConnectionString("GameStoreContext"); //Reads the connection string from configuration (Secret Manager or appsettings).

builder.Services.AddSqlServer<GameStoreContext>(connstring); //  It’s EF Core’s way of saying:“Whenever you need a GameStoreContext, I’ll create one that talks to SQL Server.”

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
