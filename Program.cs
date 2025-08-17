using GameStore.data;
using GameStore.Endpoints;
using GameStore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// time to apply the migration into our sql server instance:
// the command is this:   dotnet ef database update


builder.Services.AddSingleton<IGameRepository, InMemRepository>();

var connstring = builder.Configuration.GetConnectionString("GameStoreContext"); //Reads the connection string from configuration (Secret Manager or appsettings).

// builder.Services → gives you access to the DI container registration system.
// AddSqlServer<GameStoreContext>(connstring) → tells the container:
// “Here’s a DbContext (GameStoreContext) that connects to SQL Server using this connection string.”
// Registers it with Scoped lifetime by default (one instance per request).
builder.Services.AddSqlServer<GameStoreContext>(connstring);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    dbContext.Database.Migrate();
}

// --- Apply EF Core Migrations on Startup ---
//
// 1. Create a new scope:
//    - DbContext has Scoped lifetime (one instance per request).
//    - Program.cs runs outside any request, so we create a manual scope
//      to safely resolve scoped services.

// 2. Access the IServiceProvider from the scope:
//    - scope.ServiceProvider gives access to the DI container for this scope.
//    - The DI container actually implements IServiceProvider behind the scenes.
//    - It knows how to create instances of registered services like GameStoreContext.

// 3. Get an instance of GameStoreContext:
//    - GetRequiredService<GameStoreContext>() asks the container to provide a ready-to-use instance.
//    - Dependencies are automatically injected and lifetime is managed.
//    - Throws an error if the service is not registered.

// 4. Apply pending migrations:
//    - Ensures the database schema is up-to-date with the latest EF Core migrations.
//    - Creates the database if it does not exist.

app.MapGamesEndpoints();

app.Run();
