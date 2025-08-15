namespace GameStore.Repositories;

using GameStore.Entities;

public class InMemRepository : IGameRepository
{
    private readonly List<Game> games = new List<Game>
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

    public IEnumerable<Game> GetAllGames()
    {
        return games;
    }

    public Game? GetSpecificGame(int id)
    {
        var intendedgame = games.Find(game => game.Id == id);
        return intendedgame;
    }

    public void CreateGame(Game newgame)
    {
        newgame.Id = games.Max(game => game.Id) + 1;
        games.Add(newgame);
    }

    public Game? UpdateGame(int id, Game newgame)
    {
        int index = games.FindIndex(game => game.Id == id);

        if (index == -1)
        {
            return null;
        }
        newgame.Id = id;
        games[index] = newgame;
        return games[index];
    }

    public void DeleteGame(int id)
    {
        var game = games.Find(game => game.Id == id);
        if (game is not null)
        {
            games.Remove(game);
        }
    }
}
