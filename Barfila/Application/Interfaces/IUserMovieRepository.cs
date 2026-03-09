using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserMovieRepository
    {

        Task AddReviewAsync(UserMovie userMovie);

        Task<List<UserMovie>> GetAllWatchedMoviesAsync(Guid userId);

        Task<UserMovie> GetUserReviewAsync(Guid userId, Guid movieId);

        Task AddToWatchlistAsync(Guid userId, Guid movieId);
        Task<List<Movie>> GetWatchlistAsync(Guid userId);
        Task RemoveFromWatchlistAsync(Guid userId, Guid movieId);

        Task UpdateReviewAsync(UserMovie userMovie);

        Task<bool> HasWatchedAsync(Guid userId, Guid movieId);



    }
}
