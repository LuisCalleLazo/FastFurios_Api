using FastFurios_Api.Dtos;

namespace FastFurios_Api.Services.Interfaces
{
  public interface IAuthService
  {
    
    // AUTHENTICATION
    Task<AuthResponseDto> Authentication(PlayerDto userData);
    Task<AuthResponseDto> AuthenticationLogin(LoginRequestDto auth);
    Task<AuthResponseDto> AuthenticationGoogle(string googleId);

    // TOKEN
    Task<bool> ValidateRefreshToken(RefreshTokenRequestDto auth, int idUser);
    Task<AuthResponseDto> RefreshToken(RefreshTokenRequestDto auth, int idUser);

    // REGISTER
    Task<AuthResponseDto> RegisterPlayerGoogle(OAuthRegisterDto register);
  }
}