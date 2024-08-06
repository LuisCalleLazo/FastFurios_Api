using System.ComponentModel.DataAnnotations;
using FastFurios_Api.Models.Templates;

namespace FastFurios_Api.Models
{
  public class Player : HistoryModel
  {
    public int Id {get; set;}
    public string Name {get; set;}

    [EmailAddress]
    public string Email {get; set;}
    public string Password {get; set;}
    public Guid PasswordSalt {get; set;}
    public DateTime BirthDate { get; set; }
    public string GoogleId { get; set; } 
    public string FacebookId { get; set; } 

    // todo: Inheritances
    public virtual ICollection<ObjectItem> ObjectItems {get; set;}
    public virtual ICollection<PaymentSkin> PaymentSkins {get; set;}
    public virtual ICollection<Token> Tokens {get; set;}
  }
}