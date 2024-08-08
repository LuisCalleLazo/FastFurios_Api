namespace FastFurios_Api.Helpers
{
  public static class HelpDateActions
  {
    public static int GetAge(DateTime birthDate)
    {
      DateTime today = DateTime.Today;
      int age = today.Year - birthDate.Year;
      if (birthDate.Date > today.AddYears(-age)) age--;
      return age;
    }
  }
}