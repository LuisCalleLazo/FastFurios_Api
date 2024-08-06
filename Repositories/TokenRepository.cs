using FastFurios_Api.Database;
using FastFurios_Api.Dtos;
using FastFurios_Api.Repositories.Interfaces;
using FastFurios_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFurios_Api.Repositories
{
  public class TokenRepository : ITokenRepository
  {
    private readonly DataContext _context;
    public TokenRepository(DataContext context)
    {
      _context = context;
    }

    public async Task<Token> CreateToken(Token token, int playerId, int timeValidMin)
    {
      token.PlayerId = playerId;
      token.CreateDate = DateTime.UtcNow;
      token.ExpiredDate = DateTime.UtcNow.AddMinutes(timeValidMin);

      await _context.Token.AddAsync(token);
      await _context.SaveChangesAsync();

      return token;
    }

    public async Task<Token> GetTokenRefresh(RefreshTokenRequestDto req, int idUser)
    {
      var refresToken = await _context.Token
        .FirstOrDefaultAsync(r => 
          r.RefreshToken == req.RefreshToken && r.CurrentToken== req.TokenExpired && r.PlayerId == idUser && r.Active == true
      );

      return refresToken;
    }

    public async Task DesactiveToken(int PlayerId)
    {
      var token = await _context.Token
        .Where(t => t.PlayerId == PlayerId)
        .OrderByDescending(t => t.CreateDate)
        .FirstOrDefaultAsync();

      if(token != null)
      {
        token.Active = false;
        _context.Token.Update(token);

        await _context.SaveChangesAsync();
      }
    }

    public async Task DropToken(Guid id)
    {
      var token = await _context.Token.Where(t => t.Id == id).FirstOrDefaultAsync();

      _context.Token.Remove(token);
      await _context.SaveChangesAsync();
    }
  }
}