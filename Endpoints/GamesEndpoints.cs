namespace GameStore.Endpoints;

using GameStore.Entities;
using GameStore.Repositories;
using Microsoft.VisualBasic;

public static class GamesEndpoints
{
    /*
        Repository Pattern Summary:
    
        The Repository Pattern is used to separate the data access logic from the application logic.
        Instead of letting endpoints or services directly access the database or in-memory collections,
        we encapsulate all CRUD operations inside a repository.
    
        Benefits:
        - Encapsulation: Application logic no longer depends on the data source directly.
        - Abstraction: Allows switching databases (SQL Server, Cosmos DB, etc.) without changing endpoints.
        - Testability: Repositories can be mocked for unit testing.
        - Separation of Concerns: Keeps the API endpoints focused only on request/response handling.
    
        In this project:
        - Endpoints now call repository methods (e.g., GetAllGames, CreateGame, UpdateGame).
        - The underlying data store (currently in-memory) is hidden from the rest of the app.
        - Future migrations to a database will not require rewriting the endpoints,
          only a new repository implementation.
    */

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes) //we use this method in the program.cs: there the 'routes' is app.
    {
        var GamesGroup = routes.MapGroup("/games").WithParameterValidation();
        var mem_repo = new InMemRepository();
        GamesGroup.MapGet("/", () => mem_repo.GetAllGames()).WithName("GETALLGAMES");

        GamesGroup
            .MapGet(
                "/{id}",
                (int id) =>
                {
                    var x = mem_repo.GetSpecificGame(id);
                    return x is null
                        ? Results.NotFound(new { message = $"Game {id} not found." })
                        : Results.Ok();
                }
            )
            .WithName("GETGameBYID");

        GamesGroup
            .MapPost(
                "",
                (Game game) =>
                {
                    mem_repo.CreateGame(game);
                    return Results.CreatedAtRoute("GETGameBYID", new { id = game.Id }, game);
                }
            )
            .WithName("GamePOSTER");

        GamesGroup.MapPut(
            "/{id}",
            (int id, Game newgame) =>
            {
                var game = mem_repo.UpdateGame(id, newgame);
                if (game == null)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            }
        );

        GamesGroup.MapDelete(
            "/{id}",
            (int id) =>
            {
                mem_repo.DeleteGame(id);
                Results.NoContent();
            }
        );

        return GamesGroup;
    }
}
