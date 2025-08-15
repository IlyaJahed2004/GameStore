namespace GameStore.Repositories;
using GameStore.Entities;

// We define the IGameRepository interface to follow the principle of abstraction in software design.
// This allows us to depend on a contract (the interface) rather than a concrete implementation.
//
//  Benefits:
// - Makes the code loosely coupled and easier to maintain.
// - Supports dependency injection: the DI container can inject any class that implements this interface.
// - Improves testability: we can inject a mock or fake repository during testing instead of the real one.
// - Enables flexibility and extensibility:
//     → We can create other repositories (e.g., SqlGameRepository, FileGameRepository, ApiGameRepository)
//       that implement IGameRepository, and use them in our endpoints **without changing the endpoint code**.
//     → We only need to register the desired implementation with the DI container, like:
//
//         builder.Services.AddScoped<IGameRepository, SqlGameRepository>();
//
//     Now the DI system will automatically inject SqlGameRepository wherever IGameRepository is used.
//
// In short, the interface defines *what* the repository should do, and the classes define *how* they do it.

public interface IGameRepository
{
    void CreateGame(Game newgame);
    void DeleteGame(int id);
    IEnumerable<Game> GetAllGames();
    Game? GetSpecificGame(int id);
    Game? UpdateGame(int id, Game newgame);
}
