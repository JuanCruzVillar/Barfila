using Application.UseCases.Watchlist.Commands;
using Application.UseCases.Watchlist.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Barfila.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WatchlistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WatchlistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist([FromBody] AddToWatchlistCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWatchlist(Guid userId)
        {
            var result = await _mediator.Send(new GetWatchlistQuery { UserId = userId });
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromWatchlist([FromBody] RemoveFromWatchlistCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}