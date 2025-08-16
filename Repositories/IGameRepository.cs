namespace GameStore.Repositories;

using GameStore.Entities;

public interface IGameRepository
{
    void CreateGame(Game newgame);
    void DeleteGame(int id);
    IEnumerable<Game> GetAllGames();
    Game? GetSpecificGame(int id);
    Game? UpdateGame(int id, Game newgame);
}
