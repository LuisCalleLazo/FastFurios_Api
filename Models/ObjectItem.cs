using FastFurios_Api.Models.Templates;

namespace FastFurios_Api.Models
{
  public class ObjectItem : HistoryModel
  {
    public int Id { get; set; }
    public Guid Key { get; set; }

    // todo: Foreign Keys
    public int ObjectDetailId { get; set; }
    public int PlayerId { get; set; }

    // todo: Refs
    public virtual Player PlayerRef { get; set; }
    public virtual ObjectDetail ObjectDetailRef { get; set; }
  }
}