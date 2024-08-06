using FastFurios_Api.Dtos;
using FastFurios_Api.Models;

namespace FastFurios_Api.Repositories.Interfaces
{
  public interface ITokenRepository
  {
    
    Task<Token> CreateToken(Token token, int userId, int timeValidMin);
    Task<Token> GetTokenRefresh(RefreshTokenRequestDto req, int idUser);
    Task DesactiveToken(int userId);
    Task DropToken(Guid id);
  }
}