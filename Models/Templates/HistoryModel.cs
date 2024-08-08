namespace FastFurios_Api.Models.Templates
{
  public class HistoryModel
  {
    public DateTime CreateDate {get; set;}
    public DateTime UpdateDate {get; set;}
    public DateTime DeleteDate {get; set;}

    public HistoryModel()
    {
      CreateDate = DateTime.Now;
      UpdateDate = DateTime.MinValue;
      DeleteDate = DateTime.MinValue;
    }
  }
}