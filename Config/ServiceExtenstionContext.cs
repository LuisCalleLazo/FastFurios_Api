using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using FastFurios_Api.Database;

namespace FastFurios_Api.Config
{
  public static class ServiceExtenstionContext
  {
    public static void ConfigureContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
      string connectionAuthDb = builder.Configuration.GetConnectionString("SqlServerDb");
      
      services.AddDbContext<DataContext>(options => 
      {
        options.UseSqlServer(connectionAuthDb, sqlOptions =>
        {
          sqlOptions.CommandTimeout(60);
        });
      });
    }
  }
}