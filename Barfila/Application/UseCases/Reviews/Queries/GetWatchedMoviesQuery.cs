using Application.DTOs;
using MediatR;

namespace Application.UseCases.Reviews.Queries
{
    public record GetWatchedMoviesQuery : IRequest<List<UserMovieDto>>
    {
        public Guid UserId { get; init; }
    }
}