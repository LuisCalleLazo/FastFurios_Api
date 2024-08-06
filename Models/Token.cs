namespace FastFurios_Api.Models
{
  public class Token
  {
    public Guid Id { get; set;}
    public string CurrentToken { get; set;} 
    public string RefreshToken { get; set;} 
    public DateTime CreateDate { get; set;}
    public DateTime ExpiredDate { get; set;}
    public bool Active { get; set;}

    // todo: Foreign Keys
    public int PlayerId { get; set;}

    // todo: Refs
    public virtual Player PlayerRef {get; set;}
  }
}