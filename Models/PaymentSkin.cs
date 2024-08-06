using FastFurios_Api.Models.Templates;

namespace FastFurios_Api.Models
{
  public class PaymentSkin : HistoryModel
  {
    public int Id { get; set; }

    // todo: Foreign Keys
    public int PlayerId { get; set; }
    public int SkinId {get; set;}

    // todo: Refs
    public virtual Player PlayerRef {get; set;}
    public virtual Skin SkinRef {get; set;}
  }
}