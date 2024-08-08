using AutoMapper;
using FastFurios_Api.Database;
using FastFurios_Api.Dtos;
using FastFurios_Api.Helpers;
using FastFurios_Api.Models;
using FastFurios_Api.Repositories.Interfaces;
using FastFurios_Api.Security;
using FastFurios_Api.Services.Interfaces;

namespace FastFurios_Api.Services
{
  public class PlayerService : IPlayerService
  {

    private readonly IMapper _mapper;
    private readonly IPlayerRepository _repo;
    private readonly IAuthService _authServ;
    private readonly DataContext _context;
    public PlayerService(
      IPlayerRepository repo,
      IMapper mapper,
      IAuthService authServ,
      DataContext context
    )
    {
      _context = context;
      _mapper = mapper;
      _authServ = authServ;
      _repo = repo;
    }
    public async Task<bool> ExistEmail(string email) => await _repo.GetPlayerByEmail(email) != null ? true : false;
    public async Task<bool> ExistName(string name) => await _repo.GetPlayerByName(name) != null ? true : false;

    public async Task<AuthResponseDto> RegisterPlayer(AuthFormRegisterDto register)
    {
      Guid salt = Guid.NewGuid();
      register.Password = PasswordHash.HashPassword(register.Password, salt);

      var playerMap = _mapper.Map<Player>(register);
      playerMap.PasswordSalt = salt;
     
      using var transaction = await _context.Database.BeginTransactionAsync();
      try
      {
        await _repo.CreatePlayer(playerMap);

        var response = _mapper.Map<PlayerDto>(playerMap);
        response.Age = HelpDateActions.GetAge(playerMap.BirthDate);

        var authResponse = await _authServ.Authentication(response);
        await transaction.CommitAsync();

        return authResponse;
      }
      catch(Exception)
      {
        await transaction.RollbackAsync();
        throw;
      }
    }
  }
}