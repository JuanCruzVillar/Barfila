using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Reviews.Queries
{
    public class GetWatchedMoviesHandler : IRequestHandler<GetWatchedMoviesQuery, List<UserMovieDto>>
    {
        private readonly IUserMovieRepository _userMovieRepository;

        public GetWatchedMoviesHandler(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        public async Task<List<UserMovieDto>> Handle(GetWatchedMoviesQuery request, CancellationToken cancellationToken)
        {
            var userMovies = await _userMovieRepository.GetAllWatchedMoviesAsync(request.UserId);

            return userMovies.Select(um => new UserMovieDto
            {
                MovieTitle = um.Movie.Title,
                Rating = um.Rating,
                Review = um.Review,
                WatchedAt = um.WatchedAt,
                IsRecommended = um.IsRecommended
            }).ToList();
        }
    }
}