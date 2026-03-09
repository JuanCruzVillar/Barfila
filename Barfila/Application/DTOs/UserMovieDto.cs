using System;
using System.Collections.Generic;


namespace Application.DTOs
{
    public class UserMovieDto
    {
        public string MovieTitle { get; set; } 
        public int Rating { get; set; }
        public string Review { get; set; }
        public DateTime WatchedAt { get; set; }
        public bool IsRecommended { get; set; }
    }
}
