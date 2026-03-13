using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using MediatR;

namespace Application.UseCases.Statistics.Queries
{
    public record GetUserStatsQuery : IRequest<UserStatsDto>
    {
        public Guid UserId { get; init; }
    }
}
