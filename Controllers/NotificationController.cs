using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFurios_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using FastFurios_Api.Models;

namespace FastFurios_Api.Controllers
{
  [Route("socket/notification")]
  [ApiController]
  public class NotificationController : ControllerBase
  {
    private readonly INotificationService _service;
    public NotificationController(INotificationService service)
    {
      _service = service;
    }
    
    [HttpGet("connect")]
    public async Task<IActionResult> Connect()
    {
      if (HttpContext.WebSockets.IsWebSocketRequest)
      {
        var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        await _service.HandleWebSocketConnection(webSocket);
        return Ok();
      }
      else
      {
        return BadRequest("This is not a WebSocket request.");
      }
    }
  }
}