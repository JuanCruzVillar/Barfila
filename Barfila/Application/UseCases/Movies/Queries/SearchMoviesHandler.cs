using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using MediatR;

namespace Application.UseCases.Movies.Queries
{
    public class SearchMoviesHandler : IRequestHandler<SearchMoviesQuery, List<MovieDto>>
    {
        private readonly ITmdbService _tmdbService;

        public SearchMoviesHandler(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        public async Task<List<MovieDto>> Handle(SearchMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _tmdbService.SearchMoviesAsync(request.Title);
        }
    }
}
