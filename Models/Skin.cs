using FastFurios_Api.Models.Templates;

namespace FastFurios_Api.Models
{
  public class Skin: HistoryModel
  {
    public int Id {get; set;}
    public double Price {get; set;}
    public string Name {get; set;}
    public string Asset { get; set; }

    // todo: Inheritances
    public virtual ICollection<PaymentSkin> PaymentSkins {get; set;}
  }
}