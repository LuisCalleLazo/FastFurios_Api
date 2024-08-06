using Microsoft.AspNetCore.Cors.Infrastructure;

namespace FastFurios_Api.Config
{
  public static class ServiceExtenstionCors
  {
    public static void ConfigureCors(this IServiceCollection services, string policyName)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(
          name: policyName,
          builder =>
          {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
          });
      });
    }
  }
}