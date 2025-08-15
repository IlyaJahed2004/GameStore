using GameStore.Endpoints;
using GameStore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// The code below does the following:
// 1. Registers the mapping between the interface (IGameRepository) and the concrete class (InMemRepository)
//    in the IServiceProvider (the built-in DI container).
//    This means whenever the app needs an IGameRepository, it will automatically provide an instance of InMemRepository.
//
// 2. Because the registration uses AddSingleton, only one instance of InMemRepository is created and shared
//    throughout the lifetime of the application. This is efficient because the repository holds in-memory data,
//    and we want that data to persist for the whole server uptime.
//
// 3. This setup enables loose coupling:
//    - The GamesEndpoints.cs file only depends on the interface (IGameRepository), not the concrete class.
//    - If we want to switch to another implementation (e.g., a database repository), we can do so here
//      by changing this registration without touching the endpoint code.
//
// In summary, the DI container handles resolving the interface to the class, managing the service lifetime,
// and injecting dependencies automatically into our endpoints.
builder.Services.AddSingleton<IGameRepository, InMemRepository>();

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
