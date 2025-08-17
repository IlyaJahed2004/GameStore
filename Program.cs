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

app.Services.InitializeDb();


app.MapGamesEndpoints();

app.Run();
