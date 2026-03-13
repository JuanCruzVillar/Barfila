using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Statistics.Queries
{
    public class GetUserStatsHandler : IRequestHandler<GetUserStatsQuery, UserStatsDto>
    {
        private readonly IUserMovieRepository _userMovieRepository;

        public GetUserStatsHandler(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        public async Task<UserStatsDto> Handle(GetUserStatsQuery request, CancellationToken cancellationToken)
        {
            
            var watchedMovies = await _userMovieRepository.GetAllWatchedMoviesAsync(request.UserId); // esto trae las peliculas q vio el user

            if (!watchedMovies.Any())
                return new UserStatsDto();

           // genero favorito
            var favoriteGenre = watchedMovies
                .SelectMany(um => um.Movie.Genres.Select(g => g.Name))
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            // director favorito
            var favoriteDirector = watchedMovies
                .SelectMany(um => um.Movie.Directors.Select(d => d.Name))
                .GroupBy(d => d)
                .OrderByDescending(d => d.Count())
                .Select(d => d.Key)
                .FirstOrDefault();

            // total de peliculas vistas
            var totalMoviesWatched = watchedMovies.Count;

            // pelicula mejor rateada
            var bestRatedMovie = watchedMovies
                .OrderByDescending(um => um.Rating)
                .Select(um => um.Movie.Title)
                .FirstOrDefault();

            // distribucion de los ratings
            var ratingDistribution = Enumerable.Range(1, 10)
                .ToDictionary(
                    rating => rating,
                    rating => watchedMovies.Count(um => um.Rating == rating)
                );

            return new UserStatsDto
            {
                FavoriteGenre = favoriteGenre,
                FavoriteDirector = favoriteDirector,
                TotalMoviesWatched = totalMoviesWatched,
                BestRatedMovie = bestRatedMovie,
                RatingDistribution = ratingDistribution
            };
        }
    }
}

