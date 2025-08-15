namespace GameStore.Endpoints;

using GameStore.Entities;
using GameStore.Repositories;

public static class GamesEndpoints
{
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes) // Called from Program.cs as app.MapGamesEndpoints()
    {
        var GamesGroup = routes.MapGroup("/games").WithParameterValidation();

        //  Dependency Injection in action:
        // We're no longer manually instantiating the repository (e.g., new InMemRepository()).
        // Instead, we declare the interface (IGameRepository) as a parameter in the route handler method.
        // ASP.NET Core's built-in DI system automatically injects the concrete class (e.g., InMemRepository),
        // based on the mapping we registered in Program.cs:
        //
        // builder.Services.AddScoped<IGameRepository, InMemRepository>();
        //
        // This means: whenever the app sees a request for IGameRepository, it creates and injects an instance of InMemRepository.
        // This allows for loose coupling and makes it easy to switch to a different implementation later without modifying endpoint logic.

        GamesGroup
            .MapGet("/", (IGameRepository _Igame_repo) => _Igame_repo.GetAllGames())
            .WithName("GETALLGAMES");

        GamesGroup
            .MapGet(
                "/{id}",
                (int id, IGameRepository _Igame_repo) =>
                {
                    var x = _Igame_repo.GetSpecificGame(id);
                    return x is null
                        ? Results.NotFound(new { message = $"Game {id} not found." })
                        : Results.Ok(x);
                }
            )
            .WithName("GETGameBYID");

        GamesGroup
            .MapPost(
                "",
                (Game game, IGameRepository _Igame_repo) =>
                {
                    _Igame_repo.CreateGame(game);
                    return Results.CreatedAtRoute("GETGameBYID", new { id = game.Id }, game);
                }
            )
            .WithName("GamePOSTER");

        GamesGroup.MapPut(
            "/{id}",
            (int id, Game newgame, IGameRepository _Igame_repo) =>
            {
                var game = _Igame_repo.UpdateGame(id, newgame);
                if (game == null)
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            }
        );

        GamesGroup.MapDelete(
            "/{id}",
            (int id, IGameRepository _Igame_repo) =>
            {
                _Igame_repo.DeleteGame(id);
                return Results.NoContent();
            }
        );

        return GamesGroup;
    }
}
