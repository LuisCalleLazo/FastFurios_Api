

using FastFurios_Api.Repositories;
using FastFurios_Api.Repositories.Interfaces;
using FastFurios_Api.Services;
using FastFurios_Api.Services.Interfaces;

namespace FastFurios_Api.Config
{
  public static class LoadDependecyInjection
  {
    public static void LoadRepositories(IServiceCollection services)
    {
      services.AddScoped<IPlayerRepository, PlayerRepository>();
      services.AddScoped<ITokenRepository, TokenRepository>();
    }

    public static void LoadServices(IServiceCollection services)
    {
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IPlayerService, PlayerService>();
      services.AddScoped<INotificationService, NotificationService>();
    }
  }
}