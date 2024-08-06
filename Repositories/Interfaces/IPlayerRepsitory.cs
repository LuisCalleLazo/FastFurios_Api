using FastFurios_Api.Dtos;
using FastFurios_Api.Models;

namespace FastFurios_Api.Repositories.Interfaces
{
  public interface IPlayerRepository
  {
    Task<bool> IsPlayerDeleted(int userId);
    Task<Player> GetPlayerByAuth(LoginRequestDto auth);
    Task<Player> GetPlayerById(int id);
    Task<Player> GetPlayerByName(string name);
    Task<Player> GetPlayerByEmail(string email);
    Task<Player> GetPlayerByGoogleId(string googleId);
    Task<List<Player>> GetListPlayers();

    Task CreatePlayer(Player create);
    Task UpdatePlayer(Player update);
    Task DeletePlayer(int id);
    Task DropPlayer(int id);
  }
}