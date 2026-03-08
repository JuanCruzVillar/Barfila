
using System.Collections.Generic;


namespace Domain.Entities
{
    public class UserMovie
    {
        public Guid Id { get; private set; }
        public int Rating { get; private set; }
        public string? Review { get; private set; }
        public DateTime WatchedAt { get; private set; }
        public bool IsRecommended { get; private set; }

        // Claves foráneas
        public Guid UserId { get; private set; }
        public Guid MovieId { get; private set; }

        // Propiedades de navegación
        public User User { get; private set; }
        public Movie Movie { get; private set; }

        private UserMovie() { }

        public UserMovie(Guid userId, Guid movieId, int rating, string? review, DateTime watchedAt, bool isRecommended)
        {
            if (rating < 1 || rating > 10)
                throw new ArgumentException("El rating tiene que estar entre 1 y 10");

            Id = Guid.NewGuid();
            UserId = userId;
            MovieId = movieId;
            Rating = rating;
            Review = review;
            WatchedAt = watchedAt;
            IsRecommended = isRecommended;
        }
    }
}
