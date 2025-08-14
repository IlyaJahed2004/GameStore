/*
firstly the definition of registering:
"Register an endpoint" means tell the framework (at startup) “when a request matches this URL + method, run this handler


IEndpointRouteBuilder
What: An interface that represents something that can register endpoints (routes).
Who implements it: WebApplication, RouteGroupBuilder (and other routing hosts).
Why it matters: extension methods like MapGet, MapPost, MapGroup are defined for this interface, so anything implementing it can register routes.
Key members: DataSources, ServiceProvider (plus it’s the target for route-mapping extension methods).

RouteGroupBuilder
What: A builder object returned by app.MapGroup("/prefix").
Type: class that implements IEndpointRouteBuilder.
Purpose: group endpoints under a common route prefix and share configuration (middleware, metadata, tags, validation) for all endpoints in that group.
Typical use: var g = app.MapGroup("/games"); g.MapGet("/", ...); g.RequireAuthorization();

RouteHandlerBuilder
What: The builder returned by MapGet, MapPost, MapPut, etc.
Type: class used to configure a single endpoint.
Purpose: further configure the just-registered endpoint (name it, add metadata, filters, validation).
Common fluent methods: .WithName(...), .WithTags(...), .AddEndpointFilter(...), .Validate() (if available).


How they relate (quick)
WebApplication (implements IEndpointRouteBuilder) → app.MapGroup("/games") returns RouteGroupBuilder.
RouteGroupBuilder (also implements IEndpointRouteBuilder) → group.MapGet("/path", handler) returns RouteHandlerBuilder.
RouteHandlerBuilder lets you chain configuration for that single endpoint.
*/
namespace GameStore.Endpoints;

using System.Text.RegularExpressions;
using GameStore.Entities;

public static class GamesEndpoints
{
    static List<Game> games = new List<Game>
    {
        new Game
        {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateTime(1991, 2, 1),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 2,
            Name = "The Legend of Zelda: Ocarina of Time",
            Genre = "Adventure",
            Price = 29.99M,
            ReleaseDate = new DateTime(1998, 11, 21),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 3,
            Name = "Super Mario 64",
            Genre = "Platformer",
            Price = 24.99M,
            ReleaseDate = new DateTime(1996, 6, 23),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 4,
            Name = "Half-Life 2",
            Genre = "Shooter",
            Price = 14.99M,
            ReleaseDate = new DateTime(2004, 11, 16),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 5,
            Name = "The Witcher 3: Wild Hunt",
            Genre = "RPG",
            Price = 39.99M,
            ReleaseDate = new DateTime(2015, 5, 19),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 6,
            Name = "Minecraft",
            Genre = "Sandbox",
            Price = 26.95M,
            ReleaseDate = new DateTime(2011, 11, 18),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 7,
            Name = "Doom",
            Genre = "Shooter",
            Price = 9.99M,
            ReleaseDate = new DateTime(1993, 12, 10),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 8,
            Name = "Overwatch",
            Genre = "Shooter",
            Price = 19.99M,
            ReleaseDate = new DateTime(2016, 5, 24),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 9,
            Name = "Red Dead Redemption 2",
            Genre = "Action-Adventure",
            Price = 49.99M,
            ReleaseDate = new DateTime(2018, 10, 26),
            imageuri = "https://placehold.co/100",
        },
        new Game
        {
            Id = 10,
            Name = "Cyberpunk 2077",
            Genre = "RPG",
            Price = 59.99M,
            ReleaseDate = new DateTime(2020, 12, 10),
            imageuri = "https://placehold.co/100",
        },
    };

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes) //we use this method in the program.cs: there the 'routes' is app.
    {
        var GamesGroup = routes.MapGroup("/games").WithParameterValidation();
        GamesGroup.MapGet("/", () => games).WithName("GETALLGAMES");

        GamesGroup
            .MapGet(
                "/{id}",
                (int id) =>
                {
                    var game = games.Find(game => game.Id == id);
                    return game == null
                        ? Results.NotFound(new { message = $"Game {id} not found." })
                        : Results.Ok(game);
                }
            )
            .WithName("GETGameBYID");

        GamesGroup
            .MapPost(
                "",
                (Game game) =>
                {
                    game.Id = games.Max(game => game.Id) + 1;

                    games.Add(game);

                    return Results.CreatedAtRoute("GETGameBYID", new { id = game.Id }, game);
                }
            )
            .WithName("GamePOSTER");

        GamesGroup.MapPut(
            "/{id}",
            (int id, Game updatedgame) =>
            {
                var existing_game = games.Find(game => game.Id == id);
                if (existing_game == null)
                {
                    return Results.NotFound();
                }

                existing_game.Name = updatedgame.Name;
                existing_game.Genre = updatedgame.Genre;
                existing_game.Price = updatedgame.Price;
                existing_game.ReleaseDate = updatedgame.ReleaseDate;
                existing_game.imageuri = updatedgame.imageuri;

                return Results.NoContent();
            }
        );

        GamesGroup.MapDelete(
            "/{id}",
            (int id) =>
            {
                Game? game = games.Find(game => game.Id == id);
                if (game is not null)
                {
                    games.Remove(game);
                }
                Results.NoContent();
            }
        );

        return GamesGroup;
    }
}
