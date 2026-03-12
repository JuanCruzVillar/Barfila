using Application.DTOs;
using MediatR;

namespace Application.UseCases.Watchlist.Queries
{
    public record GetWatchlistQuery : IRequest<List<MovieDto>>
    {
        public Guid UserId { get; init; }
    }
}