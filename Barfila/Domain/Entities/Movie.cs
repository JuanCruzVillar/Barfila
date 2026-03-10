
using System.Collections.Generic;
using System.IO;


namespace Domain.Entities
{
    public class Movie
    {

        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public string Synopsis { get; private set; }

        public int ReleaseYear { get; private set; }

        public string PosterPath { get; private set; }

        public int TmdbId { get; private set; }

        public ICollection<Genre> Genres { get; private set; }

        public ICollection<Director> Directors { get; private set; }

        public ICollection<Actor> Actors{ get; private set; }

        private Movie() { }

        public Movie(string title, string synopsis, int releaseYear, int tmdbId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Synopsis = synopsis;
            ReleaseYear = releaseYear;
            TmdbId = tmdbId;
            Genres = new List<Genre>();      
            Directors = new List<Director>();
            Actors = new List<Actor>();
        }
    }
}
