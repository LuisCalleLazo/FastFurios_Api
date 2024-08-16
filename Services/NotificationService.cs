using System.Net.WebSockets;
using System.Text;
using FastFurios_Api.Services.Interfaces;

namespace FastFurios_Api.Services
{
  public class NotificationService : INotificationService
  {

    public async Task HandleWebSocketConnection(WebSocket webSocket)
    {
      var buffer = new byte[1024 * 4];
      WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

      while (!result.CloseStatus.HasValue)
      {
          var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
          var response = Encoding.UTF8.GetBytes("Mensaje recibido: " + message);
          await webSocket.SendAsync(new ArraySegment<byte>(response, 0, response.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

          result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
      }

      await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
  }
}