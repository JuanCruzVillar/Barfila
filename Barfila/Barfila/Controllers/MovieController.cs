using Application.UseCases.Reviews.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Movies.Queries;

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

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies([FromQuery] string title)
        {
            var result = await _mediator.Send(new SearchMoviesQuery { Title = title });
            return Ok(result);
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