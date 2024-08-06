using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace FastFurios_Api.Config
{
  public static class ServiceExtenstionAuth
  {
    public static void ConfigureAuth(this IServiceCollection services, WebApplicationBuilder builder)
    {
      var jwtConfig = builder.Configuration.GetSection("JwtConfig");
      
      services
      
      .AddAuthentication( conf =>
        {
          conf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          conf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
      )

      .AddJwtBearer(op => 
        {
          op.RequireHttpsMetadata = false;
          op.SaveToken = true;
          op.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]))
          };

          op.Events = new JwtBearerEvents
          {
            OnAuthenticationFailed = context =>
            {
              context.Response.StatusCode = StatusCodes.Status401Unauthorized;
              context.Response.ContentType = "application/json";
              var result = JsonConvert.SerializeObject(new { message = context.Exception.Message });
              return context.Response.WriteAsync(result);
            }
          };
        }
      )
      
      .AddGoogle(googleOptions => 
        {
          googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
          googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecretId"];
        }
      );

    }
  }
}