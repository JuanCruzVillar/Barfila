using Application.UseCases.Movies.Queries;
using Application.UseCases.Reviews.Commands;
using Application.UseCases.Statistics.Queries;
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies([FromQuery] string title)
        {
            var result = await _mediator.Send(new SearchMoviesQuery { Title = title });
            return Ok(result);
        }


        [HttpGet("recommendations/{userId}")]
        public async Task<IActionResult> GetRecommendations(Guid userId)
        {
            var result = await _mediator.Send(new GetRecommendationsQuery { UserId = userId });
            return Ok(result);
        }

        [HttpGet("stats/{userId}")]
        public async Task<IActionResult> GetUserStats(Guid userId)
        {
            var result = await _mediator.Send(new GetUserStatsQuery { UserId = userId });
            return Ok(result);
        }
    }
}