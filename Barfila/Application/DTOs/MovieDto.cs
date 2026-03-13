
using System.Collections.Generic;

namespace Application.DTOs
{
    public class MovieDto
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string ImagePath { get; set; }
        
        public int Duration { get; set; }
        
        public int TmdbId { get; set; }

        public string? Synopsis { get; set; }
        public List<string> Genres { get; set; }

        public List<string> Directors { get; set; }

        public List<string> Actors { get; set; }
    }
}
