using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Watchlist.Commands
{
    public class RemoveFromWatchlistHandler : IRequestHandler<RemoveFromWatchlistCommand, bool>
    {
        private readonly IUserMovieRepository _userMovieRepository;

        public RemoveFromWatchlistHandler(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        public async Task<bool> Handle(RemoveFromWatchlistCommand request, CancellationToken cancellationToken)
        {
            await _userMovieRepository.RemoveFromWatchlistAsync(request.UserId, request.MovieId);
            return true;
        }
    }
}