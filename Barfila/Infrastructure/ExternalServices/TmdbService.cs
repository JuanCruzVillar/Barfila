using Application.DTOs;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Infrastructure.ExternalServices
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient; // es la clase nativa de .net para los request externos
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _apiKey = configuration["Tmdb:ApiKey"]!;
            _baseUrl = configuration["Tmdb:BaseUrl"]!;
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            _httpClient = httpClient;
        }

        public async Task<List<MovieDto>> SearchMoviesAsync(string title)
        {
            var url = $"{_baseUrl}/search/movie?query={Uri.EscapeDataString(title)}";
            var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(url);
            return response?.Results.Select(MapToDto).ToList() ?? new List<MovieDto>();
        }

        public async Task<MovieDto> GetMovieByTmdbIdAsync(int tmdbId)
        {
            var url = $"{_baseUrl}/movie/{tmdbId}?append_to_response=credits";
            var movie = await _httpClient.GetFromJsonAsync<TmdbMovieDetail>(url);
            return movie == null ? null : MapDetailToDto(movie);
        }

        public async Task<List<MovieDto>> GetSimilarMoviesAsync(int tmdbId)
        {
            var url = $"{_baseUrl}/movie/{tmdbId}/similar";
            var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(url);
            return response?.Results.Select(MapToDto).ToList() ?? new List<MovieDto>();
        }

        public async Task<List<MovieDto>> GetPopularMoviesAsync()
        {
            var url = $"{_baseUrl}/movie/popular";
            var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(url);
            return response?.Results.Select(MapToDto).ToList() ?? new List<MovieDto>();
        }

        public async Task<List<MovieDto>> GetMoviesByGenreAsync(int genreId)
        {
            var url = $"{_baseUrl}/discover/movie?with_genres={genreId}";
            var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(url);
            return response?.Results.Select(MapToDto).ToList() ?? new List<MovieDto>();
        }

        // Mapeo de respuesta simple a MovieDto
        private MovieDto MapToDto(TmdbMovieResult movie) => new MovieDto
        {
            TmdbId = movie.Id,
            Title = movie.Title,
            Synopsis = movie.Overview,
            Year = DateTime.TryParse(movie.ReleaseDate, out var date) ? date.Year : 0,
            ImagePath = movie.PosterPath != null
    ? $"https://image.tmdb.org/t/p/w500{movie.PosterPath}"
    : null,
            Genres = new List<string>(),
            Directors = new List<string>(),
            Actors = new List<string>()
        };

        // Mapeo de detalle completo a MovieDto
        private MovieDto MapDetailToDto(TmdbMovieDetail movie) => new MovieDto
        {
            TmdbId = movie.Id,
            Title = movie.Title,
            Synopsis = movie.Overview,
            Year = DateTime.TryParse(movie.ReleaseDate, out var date) ? date.Year : 0,
            ImagePath = movie.PosterPath != null
    ? $"https://image.tmdb.org/t/p/w500{movie.PosterPath}"
    : null,
            Duration = movie.Runtime ?? 0,
            Genres = movie.Genres?.Select(g => g.Name).ToList() ?? new List<string>(),
            Directors = movie.Credits?.Crew
                .Where(c => c.Job == "Director")
                .Select(c => c.Name).ToList() ?? new List<string>(),
            Actors = movie.Credits?.Cast
                .Take(5)
                .Select(c => c.Name).ToList() ?? new List<string>()
        };
    }

    // Clases para deserializar la respuesta de TMDB
    internal class TmdbSearchResponse
    {
        public List<TmdbMovieResult> Results { get; set; } = new();
    }

    internal class TmdbMovieResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }
    }

    internal class TmdbMovieDetail : TmdbMovieResult
    {
        public int? Runtime { get; set; }
        public List<TmdbGenre> Genres { get; set; } = new();
        public TmdbCredits Credits { get; set; }
    }

    internal class TmdbGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class TmdbCredits
    {
        public List<TmdbCrew> Crew { get; set; } = new();
        public List<TmdbCast> Cast { get; set; } = new();
    }

    internal class TmdbCrew
    {
        public string Name { get; set; }
        public string Job { get; set; }
    }

    internal class TmdbCast
    {
        public string Name { get; set; }
    }
}