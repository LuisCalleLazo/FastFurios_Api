using System.ComponentModel.DataAnnotations;
using FastFurios_Api.Models.Templates;

namespace FastFurios_Api.Models
{
  public class ObjectDetail : HistoryModel
  {
    public int Id {get; set;}
    public string Name {get; set;}
    public string Description { get; set; }
    public string Image { get; set; }
    public TypeObject Type {get; set;}

    // todo: Inheritances
    public virtual ICollection<ObjectItem> ObjectItems {get; set;}
  }

  public enum TypeObject
  {
    Money, Gold, SpareParts
  }
}