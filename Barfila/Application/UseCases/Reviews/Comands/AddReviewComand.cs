using MediatR;
using System;
namespace Application.UseCases.Reviews.Commands
{
    public record AddReviewCommand : IRequest<bool>
    {
        public Guid UserId { get; init; }
        public Guid MovieId { get; init; }
        public int Rating { get; init; }
        public string? Review { get; init; }
        public bool IsRecommended { get; init; }
        public DateTime WatchedAt { get; init; }
    }
}