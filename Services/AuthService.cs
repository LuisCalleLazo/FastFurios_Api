using AutoMapper;
using FastFurios_Api.Dtos;
using FastFurios_Api.Models;
using FastFurios_Api.Repositories.Interfaces;
using FastFurios_Api.Security;
using FastFurios_Api.Services.Interfaces;

namespace FastFurios_Api.Services
{
  public class AuthService : IAuthService
  {
    
    private readonly ITokenRepository _tokenRepo;
    private readonly IPlayerRepository _playerRepo;
    private readonly IMapper _mapper;
    public AuthService(ITokenRepository tokenRepo, IPlayerRepository playerRepo ,IMapper mapper)
    {
      _tokenRepo = tokenRepo;
      _playerRepo = playerRepo;
      _mapper = mapper;
    }
    public AuthService()
    {
    }
    // TODO: AUTHENTICATION
    

    public async Task<AuthResponseDto> AuthenticationLogin(LoginRequestDto auth)
    {
      var player = await _playerRepo.GetPlayerByAuth(auth);
      if(player == null) return null;

      var playerData = _mapper.Map<PlayerDto>(await _playerRepo.GetPlayerById(player.Id));
      playerData.Name = player.Name;
      playerData.Email = player.Email;
      
      return await Authentication(playerData);
    }
    public async Task<AuthResponseDto> AuthenticationGoogle(string googleId)
    {
      var player = await _playerRepo.GetPlayerByGoogleId(googleId);
      if(player == null) return null;

      var playerData = _mapper.Map<PlayerDto>(await _playerRepo.GetPlayerById(player.Id));
      playerData.Name = player.Name;
      playerData.Email = player.Email;
    
      return await Authentication(playerData);
    }

    // TODO: TOKEN SECURITY
    public async Task<bool> ValidateRefreshToken(RefreshTokenRequestDto auth, int idPlayer)
    {
      var tokenValid = await _tokenRepo.GetTokenRefresh(auth, idPlayer);
      if(tokenValid == null) return false;
      else return true;
    }

    public async Task<AuthResponseDto> RefreshToken(RefreshTokenRequestDto auth, int idPlayer)
    {
      var playerData = _mapper.Map<PlayerDto>(await _playerRepo.GetPlayerById(idPlayer));
      if(playerData == null) return null;
      return await Authentication(playerData);
    }

    // TODO: REGISTER BY OAUTH
    public async Task<AuthResponseDto> RegisterPlayerGoogle(OAuthRegisterDto register)
    {
      var player = _mapper.Map<Player>(register);
      player.PasswordSalt = Guid.NewGuid();
      await _playerRepo.CreatePlayer(player);

      var playerData = _mapper.Map<PlayerDto>(await _playerRepo.GetPlayerById(player.Id));
      playerData.Name = player.Name;
      playerData.Email = player.Email;
      
      if(playerData == null) return null;

      return await Authentication(playerData);
    }

    
    public async Task<AuthResponseDto> Authentication(PlayerDto playerData)
    {
      var jwt = JwtSecurity.GetJwtConfig();

      var response = new AuthResponseDto 
      {
        Player = playerData,
        CurrentToken = JwtSecurity.GenerateToken(jwt, playerData.Id),
        RefreshToken = JwtSecurity.GenerateRefreshToken(),
      };

      await _tokenRepo.DesactiveToken(playerData.Id);
      await _tokenRepo.CreateToken(_mapper.Map<Token>(response), playerData.Id, jwt.TimeValidMin);
      
      return response;
    }
  }
}