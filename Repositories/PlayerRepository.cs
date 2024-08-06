using Microsoft.EntityFrameworkCore;
using FastFurios_Api.Database;
using FastFurios_Api.Dtos;
using FastFurios_Api.Models;
using FastFurios_Api.Security;
using FastFurios_Api.Repositories.Interfaces;
using System;

namespace FastFurios_Api.Repositories
{
  public class PlayerRepository : IPlayerRepository
  {
    private readonly DataContext _context;

    public PlayerRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<bool> IsPlayerDeleted(int playerId) =>
      await _context
        .Player
        .FirstOrDefaultAsync(u => u.Id == playerId && u.DeleteDate == DateTime.MinValue) != null ? false : true;
        
    public async Task<Player> GetPlayerByAuth(LoginRequestDto auth)
    {
      var player = await _context.Player
        .Where(u => u.Name == auth.NameOrGmail || u.Email == auth.NameOrGmail)
        .FirstOrDefaultAsync();

      if (player == null) return null;

      if(!PasswordHash.VerifyPassword(player.Password, auth.Password, player.PasswordSalt)) return null;

      return player;
    }

    public async Task<Player> GetPlayerById(int id) => 
      await _context
        .Player
        .FirstOrDefaultAsync(u => u.Id == id);
    
    public async Task<Player> GetPlayerByName(string name) =>
      await _context
        .Player
        .FirstOrDefaultAsync(u => u.Name == name);
        
    public async Task<Player> GetPlayerByEmail(string email) => 
      await _context
        .Player
        .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<Player> GetPlayerByGoogleId(string googleId) => 
      await _context
        .Player
        .FirstOrDefaultAsync(u => u.GoogleId == googleId);

        
    public async Task<List<Player>> GetListPlayers() => 
      await _context
        .Player
        .Where(u => u.DeleteDate != DateTime.MinValue)
        .ToListAsync();

    public async Task CreatePlayer(Player create)
    {
      await _context.Player.AddAsync(create);
      await _context.SaveChangesAsync();
    }
    public async Task UpdatePlayer(Player update)
    {
      _context.Player.Update(update);
      await _context.SaveChangesAsync();
    }

    public async Task DeletePlayer(int id)
    {
      var register = await GetPlayerById(id);
      register.DeleteDate = DateTime.Now;

      _context.Player.Update(register);
      await _context.SaveChangesAsync();
    }

    public async Task DropPlayer(int id)
    {
      var player = await GetPlayerById(id);
      
      _context.Player.Remove(player);
      await _context.SaveChangesAsync();
    }

  }
}