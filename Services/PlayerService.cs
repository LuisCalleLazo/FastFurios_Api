using AutoMapper;
using FastFurios_Api.Repositories.Interfaces;
using FastFurios_Api.Services.Interfaces;

namespace FastFurios_Api.Services
{
  public class PlayerService : IPlayerService
  {

    private readonly IMapper _mapper;
    private readonly IPlayerRepository _repo;
    public PlayerService(
      IPlayerRepository repo,
      IMapper mapper
    )
    {
      _mapper = mapper;
      _repo = repo;
    }
    public async Task<bool> ExistEmail(string email) => await _repo.GetPlayerByEmail(email) != null ? true : false;
    public async Task<bool> ExistName(string name) => await _repo.GetPlayerByName(name) != null ? true : false;
  }
}