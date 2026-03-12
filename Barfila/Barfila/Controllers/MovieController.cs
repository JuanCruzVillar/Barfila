using Application.UseCases.Reviews.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barfila.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("review")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest("El usuario ya registró esta película.");

            return Ok("Reseña agregada correctamente.");
        }
    }
}