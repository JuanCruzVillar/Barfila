using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barfila.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var result = await _mediator.Send(new GetUserQuery { UserId = id });
            if (result == null)
                return NotFound("Usuario no encontrado.");
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound("Usuario no encontrado.");
            return Ok("Usuario actualizado correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserCommand { UserId = id });
            if (!result)
                return NotFound("Usuario no encontrado.");
            return Ok("Usuario eliminado correctamente.");
        }
    }
}