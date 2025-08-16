using GameStore.Endpoints;
using GameStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameRepository, InMemRepository>();

// GameStoreContext:
//   - This is the DbContext class used by Entity Framework Core to interact with the database.
//   - The connection string for GameStoreContext is NOT stored in appsettings.json (to avoid exposing secrets).
//   - Instead, it is stored securely using Secret Manager.
//   - At runtime, builder.Configuration pulls the connection string from Secret Manager into IConfiguration.

// important note:check the appsettings.json: the connection string is deleted.
builder.Configuration.GetConnectionString("GameStoreContext");

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
