using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserStatsDto
    {

        public string FavoriteGenre { get; set; }

        public string FavoriteDirector { get; set; }

        public int TotalMoviesWatched { get; set; }

        public string BestRatedMovie { get; set; }


        public Dictionary<int, int> RatingDistribution { get; set; }

    }
}
