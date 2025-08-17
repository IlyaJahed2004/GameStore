using GameStore.data;
using GameStore.Endpoints;
using GameStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// time to apply the migration into our sql server instance:
// the command is this:   dotnet ef database update


builder.Services.AddSingleton<IGameRepository, InMemRepository>();

var connstring = builder.Configuration.GetConnectionString("GameStoreContext"); //Reads the connection string from configuration (Secret Manager or appsettings).

builder.Services.AddSqlServer<GameStoreContext>(connstring); //  It’s EF Core’s way of saying:“Whenever you need a GameStoreContext, I’ll create one that talks to SQL Server.”

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
