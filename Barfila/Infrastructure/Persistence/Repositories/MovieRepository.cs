using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Directors)
                .Include(m => m.Actors)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            return await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Directors)
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == id); //trae primer resultado q matchee la condidicion, en este caso el id
        }

        public async Task<Movie> GetMovieByNameAsync(string title)
        {
            return await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Directors)
                .FirstOrDefaultAsync(m => m.Title.Contains(title)); // el contains es para generar como un LIKE '%title%' en sql, permite busqueda "parcial"
        }

        public async Task<bool> ExistsAsync(int tmdbId)
        {
            return await _context.Movies
                .AnyAsync(m => m.TmdbId == tmdbId);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Movies
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}