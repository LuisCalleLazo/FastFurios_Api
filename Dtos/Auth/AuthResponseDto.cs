
namespace FastFurios_Api.Dtos
{
  public class AuthResponseDto
  {
    public PlayerDto Player {get; set;}
    public string CurrentToken {get; set;}
    public string RefreshToken {get; set;}
  }
}