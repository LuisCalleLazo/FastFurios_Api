using AutoMapper;
using FastFurios_Api.Dtos;
using FastFurios_Api.Models;
namespace FastFurios_Api.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      // todo: Player
      CreateMap<AuthFormRegisterDto, Player>();
      CreateMap<Player, PlayerDto>();


      // todo: Token
      CreateMap<AuthResponseDto, Token>();
    }
  }
}