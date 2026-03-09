using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Reviews.Commands
{
    public class AddReviewHandler : IRequestHandler<AddReviewCommand, bool>
    {
        private readonly IUserMovieRepository _userMovieRepository;

        public AddReviewHandler(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        public async Task<bool> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            // verificar si vio la peli
            var alreadyWatched = await _userMovieRepository.HasWatchedAsync(request.UserId, request.MovieId);

            //  si ya la vio, no hacemos nada
            if (alreadyWatched)
                return false;

            // crear la entidad 
            var userMovie = new UserMovie(
                request.UserId,
                request.MovieId,
                request.Rating,
                request.Review,
                request.WatchedAt,
                request.IsRecommended
            );

            // addear la review
            await _userMovieRepository.AddReviewAsync(userMovie);

            
            return true;
        }
    }
}