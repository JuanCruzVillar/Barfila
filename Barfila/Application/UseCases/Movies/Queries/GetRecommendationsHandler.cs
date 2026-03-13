using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Movies.Queries
{
    public class GetRecommendationsHandler : IRequestHandler<GetRecommendationsQuery, List<MovieDto>>
    {
        private readonly IUserMovieRepository _userMovieRepository;
        private readonly ITmdbService _tmdbService;
        private readonly ICacheService _cacheService;

        public GetRecommendationsHandler(
            IUserMovieRepository userMovieRepository,
            ITmdbService tmdbService,
            ICacheService cacheService)
        {
            _userMovieRepository = userMovieRepository;
            _tmdbService = tmdbService;
            _cacheService = cacheService;
        }

        public async Task<List<MovieDto>> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
        {
            // obtener desde cache
            var cacheKey = $"recommendations:{request.UserId}";
            var cached = await _cacheService.GetAsync<List<MovieDto>>(cacheKey);
            if (cached != null)
                return cached;

            // esto trae peliculas vistas con rating mayor o igual a 7
            var watchedMovies = await _userMovieRepository.GetAllWatchedMoviesAsync(request.UserId);
            var topRated = watchedMovies.Where(um => um.Rating >= 7).ToList();

            if (!topRated.Any())
                return new List<MovieDto>();

            //  generos y directores
            var favoriteGenres = topRated
                .SelectMany(um => um.Movie.Genres.Select(g => g.Name))
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            var favoriteDirectors = topRated
                .SelectMany(um => um.Movie.Directors.Select(d => d.Name))
                .GroupBy(d => d)
                .OrderByDescending(d => d.Count())
                .Take(3)
                .Select(d => d.Key)
                .ToList();

            // buscar peliculas similares en TMDB
            var watchedTmdbIds = watchedMovies.Select(um => um.Movie.TmdbId).ToHashSet();
            var candidates = new List<MovieDto>();

            foreach (var movie in topRated.Take(3))
            {
                var similar = await _tmdbService.GetSimilarMoviesAsync(movie.Movie.TmdbId);
                var unseen = similar.Where(m => !watchedTmdbIds.Contains(m.TmdbId));
                candidates.AddRange(unseen);
            }

            
            var scored = candidates
                .GroupBy(m => m.TmdbId)
                .Select(g => g.First())
                .Select(movie => new
                {
                    Movie = movie,
                    Score = CalculateScore(movie, favoriteGenres, favoriteDirectors)
                })
                .OrderByDescending(x => x.Score)
                .Take(10)
                .Select(x => x.Movie)
                .ToList();

            // cachear resultado 24 horas
            await _cacheService.SetAsync(cacheKey, scored, TimeSpan.FromHours(24));

            return scored;
        }

        private int CalculateScore(MovieDto movie, List<string> favoriteGenres, List<string> favoriteDirectors)
        {
            int score = 0;

            
            score += movie.Genres.Count(g => favoriteGenres.Contains(g)) * 2; // mas 2 si es del mismo genero, y mas 3 si es del mismo director

            
            score += movie.Directors.Count(d => favoriteDirectors.Contains(d)) * 3;

            return score;
        }
    }
}