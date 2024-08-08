using FastFurios_Api.Dtos;

namespace FastFurios_Api.Services.Interfaces
{
  public interface IPlayerService 
  {
    Task<bool> ExistEmail(string email);
    Task<bool> ExistName(string name);
    Task<AuthResponseDto> RegisterPlayer(AuthFormRegisterDto register);
  }
}