using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Auth.Commands;

namespace Barfila.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("El email ya está registrado.");
            return Ok("Usuario registrado correctamente.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return Unauthorized("Email o contraseña incorrectos.");
            return Ok(result);
        }
    }
}