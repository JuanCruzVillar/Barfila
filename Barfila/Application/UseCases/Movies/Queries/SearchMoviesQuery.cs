using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using MediatR;

namespace Application.UseCases.Movies.Queries
{
    public record SearchMoviesQuery : IRequest<List<MovieDto>>
    {
        public string Title { get; init; }
    }
}
