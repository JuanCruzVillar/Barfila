using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Watchlist
    {
        public Guid Id { get; private set; }

        public DateTime AddedAt { get; private set; }
        // FK
        public Guid UserId { get; private set; }
        public Guid MovieId { get; private set; }

        // propiedades de navegacion
        public User User { get; private set; }
        public Movie Movie { get; private set; }

        private Watchlist () { }

        public Watchlist(Guid userId, Guid movieId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            MovieId = movieId;
            AddedAt = DateTime.UtcNow;
        }
    }
}
