
using FastFurios_Api.Dtos;
using FastFurios_Api.Helpers;
using FastFurios_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FastFurios_Api.Controllers
{
  [Route("api/player")]
  [ApiController]
  public class PlayerController : ControllerBase
  {
    private string message_error = "Hubo un error, consulte con el administrador";
    private readonly ILogger<PlayerController> _logger;
    private readonly IPlayerService _service;
    public PlayerController(ILogger<PlayerController> logger, IPlayerService service)
    {
      _logger = logger;
      _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterPlayer(AuthFormRegisterDto register)
    {
      try
      {
        
        if(await _service.ExistEmail(register.Email)) return BadRequest("El email ya existe");
        if(await _service.ExistName(register.Name)) return BadRequest("El nombre ya existe");

        if (HelpDateActions.GetAge(register.BirthDate) < 18) 
          return BadRequest("No tienes la edad suficiente para este juego");

        var auth_register = await _service.RegisterPlayer(register);
        
        if (auth_register == null) 
          return BadRequest("No se pudo el registro");
          
        return Ok(auth_register);

      }
      catch(Exception err)
      {
        _logger.LogError(err.Message);
        Console.WriteLine(err.StackTrace);
        return BadRequest(message_error);
      }
    }

    [HttpPut]
    public async Task<IActionResult> PutTModel()
    {
      await Task.Yield();

      return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTModelById(int id)
    {
      await Task.Yield();

      return null;
    }
  }
}