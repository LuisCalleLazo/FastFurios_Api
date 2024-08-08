using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FastFurios_Api.Dtos;
using FastFurios_Api.Services.Interfaces;

namespace FastFurios_Api.Controllers
{
  [Route("api/auth")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private string message_error = "Hubo un error, consulte con el administrador";
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _service;
    private readonly IPlayerService _playerServ;

    public AuthController(
      IAuthService service,
      IPlayerService playerServ,
      ILogger<AuthController> logger
    )
    {
      _logger = logger;
      _service = service;
      _playerServ = playerServ;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm]LoginRequestDto auth)
    {
      try
      {
        var authentication =  await _service.AuthenticationLogin(auth);
        if(authentication == null) return BadRequest("Acceso denegado");

        return Ok(authentication);

      }
      catch(Exception err)
      {
        _logger.LogError(err.Message);
        Console.WriteLine(err.StackTrace);
        return BadRequest(message_error);
      }
    }

    [HttpPost("oauth-google")]
    public async Task<IActionResult> OAuthGoogle([FromBody] string token)
    {
      try
      {
        var payload = await GoogleJsonWebSignature.ValidateAsync(token);
        
        if (payload == null) return BadRequest("Invalid Google Token");

        var authentication = new AuthResponseDto();
        authentication =  await _service.AuthenticationGoogle(payload.Subject);

        Random random = new Random();
        string nameGoogle = payload.Name.Split(' ')[0] ?? "null" + "_name_" + random.Next(1, 999);

        if(await _playerServ.ExistEmail(payload.Email)) return BadRequest("El email ya existe");
        if(await _playerServ.ExistName(nameGoogle)) return BadRequest("El nombre ya existe");

        if(authentication == null)
        { 
          authentication = await _service.RegisterPlayerGoogle(new OAuthRegisterDto 
          {
            Name = nameGoogle,
            Email = payload.Email,
            GoogleId = payload.Subject
          });
        }

        return Ok(authentication);
        
      }
      catch(Exception err)
      {
        _logger.LogError(err.Message);
        Console.WriteLine(err.StackTrace);
        return BadRequest(message_error);
      }
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto refresh)
    {
      try
      {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenExpired = tokenHandler.ReadJwtToken(refresh.TokenExpired);

        if(tokenExpired.ValidTo > DateTime.UtcNow)
          return BadRequest("Token no ha expirado");


        int IdPlayer = Int32.Parse(tokenExpired.Claims.First(x => x.Type == "id").Value);

        if(!await _service.ValidateRefreshToken(refresh, IdPlayer))
          return BadRequest("El token y refresh token no son invalidos");

        var authResponse = await _service.RefreshToken(refresh, IdPlayer);
        
        return Ok(authResponse);
        
      }catch(Exception err)
      {
        _logger.LogError(err.Message);
        Console.WriteLine(err.StackTrace);
        return BadRequest(message_error);
      }
    }
  }
}