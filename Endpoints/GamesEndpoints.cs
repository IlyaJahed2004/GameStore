namespace GameStore.Endpoints;

using GamesStore.Dtos;
using GameStore.Entities;
using GameStore.Repositories;

public static class GamesEndpoints
{
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes) // Called from Program.cs as app.MapGamesEndpoints()
    {
        var GamesGroup = routes.MapGroup("/games").WithParameterValidation();

        GamesGroup
            .MapGet(
                "/",
                (IGameRepository _Igame_repo) =>
                    _Igame_repo.GetAllGames().Select(game => game.AsDto())
            )
            .WithName("GETALLGAMES");

        GamesGroup
            .MapGet(
                "/{id}",
                (int id, IGameRepository _Igame_repo) =>
                {
                    var x = _Igame_repo.GetSpecificGame(id);
                    return x is null
                        ? Results.NotFound(new { message = $"Game {id} not found." })
                        : Results.Ok(x.AsDto());
                }
            )
            .WithName("GETGameBYID");

        GamesGroup
            .MapPost(
                "",
                (CreateGameDto game_dto, IGameRepository _Igame_repo) => //we are client and we post gamedto not a game actually.so we should convert it to a game and add to the database.
                {
                    Game game = new Game()
                    {
                        Name = game_dto.Name,
                        Genre = game_dto.Genre,
                        Price = game_dto.Price,
                        ReleaseDate = game_dto.ReleaseDate,
                        imageuri = game_dto.imageuri,
                    };
                    _Igame_repo.CreateGame(game);
                    return Results.CreatedAtRoute("GETGameBYID", new { id = game.Id }, game);
                }
            )
            .WithName("GamePOSTER");

        GamesGroup.MapPut(
            "/{id}",
            (int id, UpdateGameDto updatedgame_dto, IGameRepository _Igame_repo) =>
            {
                Game updated_game = new()
                {
                    Name = updatedgame_dto.Name,
                    Genre = updatedgame_dto.Genre,
                    Price = updatedgame_dto.Price,
                    ReleaseDate = updatedgame_dto.ReleaseDate,
                    imageuri = updatedgame_dto.imageuri,
                };
                var game = _Igame_repo.UpdateGame(id, updated_game);
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
