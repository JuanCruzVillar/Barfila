using Application.UseCases.Reviews.Commands;
using Application.UseCases.Reviews.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barfila.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] AddReviewCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest("El usuario ya registró esta película.");
            return Ok("Reseña agregada correctamente.");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetWatchedMovies(Guid userId)
        {
            var result = await _mediator.Send(new GetWatchedMoviesQuery { UserId = userId });
            return Ok(result);
        }
    }
}