using GameStore.Entities;

// Sample list of games to serve as our in-memory data source
var games = new List<Game>
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

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//after defining the data annotations we should validate the resources posted or putted with the syntax withparametervalidation which we have got from the nuget package named minimalapis extension.
var GamesGroup = app.MapGroup("/games").WithParameterValidation();

/// <summary>
/// Route: GET /games
/// Handler: Returns the full list of games as JSON.
/// - The route "/games" maps to this handler.
/// - No parameters are needed here.
/// Important note:i didnt use route map just because to realize this mode without using kind of syntax:
/// </summary>


GamesGroup.MapGet("/", () => games).WithName("GETALLGAMES"); // Naming the endpoint.

/// <summary>
/// Route: GET /games/{id}
/// Handler: Returns a single game by its ID.
/// - The "{id}" part is a route parameter captured from the URL.
/// - The framework automatically converts this segment to an int and passes it to the handler.
/// - The handler searches the 'games' list for a matching game.
/// - If found, returns HTTP 200 OK with the game data in JSON.
/// - If not found, returns HTTP 404 Not Found with a helpful message.
/// </summary>
GamesGroup
    .MapGet(
        "/{id}",
        (int id) =>
        {
            // Find the first game where the Id matches the route parameter 'id'
            var game = games.Find(game => game.Id == id);

            // Conditional return:
            // If game is null (not found), return 404 Not Found with JSON message
            // Otherwise, return 200 OK with the game serialized as JSON
            return game == null
                ? Results.NotFound(new { message = $"Game {id} not found." })
                : Results.Ok(game);
        }
    )
    .WithName("GETGameBYID"); // Named route used by CreatedAtRoute

/// <summary>
/// Registers a POST endpoint at {baseUrl}/games
/// Example: http://localhost:5000/games
/// Only POST requests will trigger this handler (not GET, PUT, etc.).
/// This endpoint accepts a Game object from the request body, assigns a new Id,
/// adds it to the games list, and returns a 201 Created response.
///
/// The Results.CreatedAtRoute method:
/// - Sets the HTTP status code to 201 Created,
/// - Adds a Location header with the URL of the newly created resource,
///   generated dynamically from the named route "GETGameBYID",
/// - Returns the created game object as JSON in the response body,
/// so the client knows exactly where to retrieve the new game.
/// </summary>
GamesGroup
    .MapPost(
        "",
        (Game game) =>
        {
            // Assign a new Id (max existing Id + 1)
            game.Id = games.Max(game => game.Id) + 1;

            // Add the new game to the in-memory list (our simple database)
            games.Add(game);

            // Return 201 Created response with Location header and response body
            return Results.CreatedAtRoute(
                "GETGameBYID", // Named route from GET /games/{id}
                new { id = game.Id }, // Route values for URL generation (anonymous object)
                game // Response body (the newly created game)
            );
        }
    )
    .WithName("GamePOSTER"); // Naming the POST endpoint

GamesGroup.MapPut(
    "/{id}",
    (int id, Game updatedgame) =>
    {
        var existing_game = games.Find(game => game.Id == id);
        if (existing_game == null)
        {
            return Results.NotFound();
        }

        // Do NOT update the Id, keep the original one from the route
        existing_game.Name = updatedgame.Name;
        existing_game.Genre = updatedgame.Genre;
        existing_game.Price = updatedgame.Price;
        existing_game.ReleaseDate = updatedgame.ReleaseDate;
        existing_game.imageuri = updatedgame.imageuri;

        return Results.NoContent(); // 204 No Content - successful update without body
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

app.Run();
