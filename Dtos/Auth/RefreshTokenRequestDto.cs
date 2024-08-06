namespace FastFurios_Api.Dtos
{
  public class RefreshTokenRequestDto
  {
    public string TokenExpired {get; set;} 
    public string RefreshToken {get; set;} 
  }
}