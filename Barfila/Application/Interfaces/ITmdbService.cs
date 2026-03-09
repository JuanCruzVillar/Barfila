using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITmdbService
    {
        Task<List<MovieDto>> SearchMoviesAsync(string title);
        Task<MovieDto> GetMovieByTmdbIdAsync(int tmdbId);
        Task<List<MovieDto>> GetSimilarMoviesAsync(int tmdbId);
        Task<List<MovieDto>> GetPopularMoviesAsync();
        Task<List<MovieDto>> GetMoviesByGenreAsync(int genreId);
    }
}