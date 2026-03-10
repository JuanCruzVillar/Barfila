using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserMovieRepository : IUserMovieRepository
    {
        private readonly AppDbContext _context;

        public UserMovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(UserMovie userMovie)
        {
            await _context.UserMovies.AddAsync(userMovie);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserMovie>> GetAllWatchedMoviesAsync(Guid userId)
        {
            return await _context.UserMovies
                .Include(um => um.Movie)
                .Where(um => um.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserMovie> GetUserReviewAsync(Guid userId, Guid movieId)
        {
            return await _context.UserMovies
                .Include(um => um.Movie)
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);
        }

        public async Task<bool> HasWatchedAsync(Guid userId, Guid movieId)
        {
            return await _context.UserMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);
        }

        public async Task UpdateReviewAsync(UserMovie userMovie)
        {
            _context.UserMovies.Update(userMovie);
            await _context.SaveChangesAsync();
        }

        public async Task AddToWatchlistAsync(Guid userId, Guid movieId)
        {
            var watchlistItem = new Watchlist(userId, movieId);
            await _context.Watchlists.AddAsync(watchlistItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetWatchlistAsync(Guid userId)
        {
            return await _context.Watchlists
                .Where(w => w.UserId == userId)
                .Select(w => w.Movie)
                .ToListAsync();
        }

        public async Task RemoveFromWatchlistAsync(Guid userId, Guid movieId)
        {
            var item = await _context.Watchlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.MovieId == movieId);

            if (item != null)
            {
                _context.Watchlists.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}   