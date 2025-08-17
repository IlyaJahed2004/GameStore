using GameStore.data;
using GameStore.Endpoints;
using GameStore.Repositories;

var builder = WebApplication.CreateBuilder(args);


// in this level , we generated the first database migration:
// we should use these commands:
// dotnet tool install --global dotnet-ef
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet ef migrations add InitialCreate --output-dir Data\Migrations

// EF Core migrations summary:
// 1. Entity classes (e.g. Game) define the structure of data (columns).
// 2. DbSet<TEntity> in DbContext (e.g. DbSet<Game> games) tells EF Core
//    to include that entity in the model → usually becomes a table.
// 3. The "model" in EF Core = EF’s internal blueprint built from:
//      - entity classes
//      - DbSet properties in DbContext
//      - data annotations and Fluent API configurations
//    → EF uses this model to know how to generate database tables/columns.
// 4. `dotnet ef migrations add <Name>` generates a migration file
//    (C# code with Up/Down methods) based on the model.
// 5. `dotnet ef database update` applies the migration by running SQL
//    (e.g. CREATE TABLE games) against the database.
// → Model = EF’s internal representation; Database = actual tables created from it.


builder.Services.AddSingleton<IGameRepository, InMemRepository>();

var connstring = builder.Configuration.GetConnectionString("GameStoreContext"); //Reads the connection string from configuration (Secret Manager or appsettings).

builder.Services.AddSqlServer<GameStoreContext>(connstring); //  It’s EF Core’s way of saying:“Whenever you need a GameStoreContext, I’ll create one that talks to SQL Server.”

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
