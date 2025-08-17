using GameStore.data;
using GameStore.Endpoints;
using GameStore.Repositories;

var builder = WebApplication.CreateBuilder(args);


// -------------------- Notes on Program.cs --------------------

// builder.Services.AddSqlServer<GameStoreContext>(connstring);
//   • Registers GameStoreContext with dependency injection.
//   • Tells EF Core to use SQL Server as the provider.
//   • Links GameStoreContext to the real SQL Server database using connstring.
//
// builder.Services.AddSingleton<IGameRepository, InMemRepository>();
//   • Registers InMemRepository as the implementation of IGameRepository.
//   • This means the app still uses an in-memory repository, not SQL Server.
//   • No conflict with AddSqlServer (they’re independent).
//   • ⚠ If you want the app to use SQL Server instead of memory:
//        - Create an EfGameRepository that uses GameStoreContext.
//        - Register it with DI like:
//            builder.Services.AddScoped<IGameRepository, EfGameRepository>();
// --------------------------------------------------------------


builder.Services.AddSingleton<IGameRepository, InMemRepository>();

var connstring = builder.Configuration.GetConnectionString("GameStoreContext"); //Reads the connection string from configuration (Secret Manager or appsettings).

builder.Services.AddSqlServer<GameStoreContext>(connstring); //  It’s EF Core’s way of saying:“Whenever you need a GameStoreContext, I’ll create one that talks to SQL Server.”

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
