using System.Net.WebSockets;

namespace FastFurios_Api.Services.Interfaces
{
  public interface INotificationService
  {
    Task HandleWebSocketConnection(WebSocket webSocket);
  }
}