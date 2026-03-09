using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(Guid id);
        Task AddMovieAsync(Movie movie);

        Task<Movie> GetMovieByNameAsync(string title);

        Task<bool> ExistsAsync(int tmdbId);

        Task UpdateAsync(Movie movie);

        Task<List<Movie>> GetPagedAsync(int page, int pageSize);
    }
}
