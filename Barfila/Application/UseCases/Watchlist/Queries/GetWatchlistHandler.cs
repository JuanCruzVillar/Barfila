using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Watchlist.Queries
{
    public class GetWatchlistHandler : IRequestHandler<GetWatchlistQuery, List<MovieDto>>
    {
        private readonly IUserMovieRepository _userMovieRepository;

        public GetWatchlistHandler(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        public async Task<List<MovieDto>> Handle(GetWatchlistQuery request, CancellationToken cancellationToken)
        {
            var movies = await _userMovieRepository.GetWatchlistAsync(request.UserId);

            return movies.Select(m => new MovieDto
            {
                Title = m.Title,
                Year = m.ReleaseYear,
                ImagePath = m.PosterPath,
                TmdbId = m.TmdbId,
                Genres = m.Genres.Select(g => g.Name).ToList(),
                Directors = m.Directors.Select(d => d.Name).ToList(),
                Actors = m.Actors.Select(a => a.Name).ToList()
            }).ToList();
        }
    }
}