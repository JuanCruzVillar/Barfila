using MediatR;

namespace Application.UseCases.Watchlist.Commands
{
    public record RemoveFromWatchlistCommand : IRequest<bool>
    {
        public Guid UserId { get; init; }
        public Guid MovieId { get; init; }
    }
}