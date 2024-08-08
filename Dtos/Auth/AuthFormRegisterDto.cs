namespace FastFurios_Api.Dtos
{
  public class AuthFormRegisterDto
  {
    public string Name {get; set;} 
    public string Email { get; set; }
    public string Password { get; set; } 
    public Guid PasswordSalt { get; set; }
    public DateTime BirthDate {get; set;}
  }

  public class OAuthRegisterDto : AuthFormRegisterDto
  {
    public string GoogleId { get; set; }
    public string FacebookId { get; set; }

  }
}