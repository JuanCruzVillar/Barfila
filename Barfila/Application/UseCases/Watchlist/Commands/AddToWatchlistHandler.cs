using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Watchlist.Commands
{
    public class AddToWatchlistHandler : IRequestHandler<AddToWatchlistCommand, bool>
    {
        private readonly IUserMovieRepository _userMovieRepository;

        public AddToWatchlistHandler(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        public async Task<bool> Handle(AddToWatchlistCommand request, CancellationToken cancellationToken)
        {
            await _userMovieRepository.AddToWatchlistAsync(request.UserId, request.MovieId);
            return true;
        }
    }
}